using Microsoft.AspNetCore.Mvc;
using Proyecto.Api.Data;
using Proyecto.Api.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Proyecto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly DbService _db;

        public InvoicesController(DbService db) => _db = db;

        // GET /api/invoices
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? clientId = null)
        {
            string where = "WHERE 1=1";
            var p = new Dictionary<string, object>();
            if (clientId.HasValue)
            {
                where += " AND ClientId = @cliId";
                p["@cliId"] = clientId;
            }

            var dt = await _db.QueryAsync(
                $"SELECT Id, InvoiceNumber, QuoteId, ClientId, IssueDate, DueDate, Subtotal, TotalTax, ReteFuente, Total, PaymentStatus FROM Invoices {where} ORDER BY Id DESC", p);
            return Ok(DbService.ToList(dt));
        }

        // GET /api/invoices/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dt = await _db.QueryAsync(
                "SELECT Id, InvoiceNumber, QuoteId, ClientId, IssueDate, DueDate, Subtotal, TotalTax, ReteFuente, Total, PaymentStatus FROM Invoices WHERE Id = @id",
                new Dictionary<string, object> { ["@id"] = id });

            if (dt.Rows.Count == 0) return NotFound();
            return Ok(DbService.ToList(dt)[0]);
        }

        // POST /api/invoices
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceRequest req)
        {
            if (req.ClientId <= 0 || req.Subtotal <= 0)
                return BadRequest("ClientId y Subtotal requeridos.");

            decimal tax = req.Subtotal * 0.19m;
            decimal reteFuente = Math.Round(req.Subtotal * (req.ReteFuentePct ?? 0.025m), 2);
            decimal total = req.Subtotal + tax - reteFuente;

            int invoiceNumber = await NextInvoiceNumber();

            bool ok = await _db.ExecuteAsync(
                "INSERT INTO Invoices (InvoiceNumber, QuoteId, ClientId, IssueDate, DueDate, Subtotal, TotalTax, ReteFuente, Total, PaymentStatus) " +
                "VALUES (@number, @qId, @cId, GETDATE(), @due, @sub, @tax, @rete, @total, 'Por Cobrar')",
                new Dictionary<string, object>
                {
                    ["@number"] = invoiceNumber,
                    ["@qId"] = req.QuoteId,
                    ["@cId"] = req.ClientId,
                    ["@due"] = req.DueDate,
                    ["@sub"] = req.Subtotal,
                    ["@tax"] = tax,
                    ["@rete"] = reteFuente,
                    ["@total"] = total
                });

            if (ok) await AuditHelper.LogAsync(_db, "api", "CREATE", "Invoices", invoiceNumber.ToString(), $"Cliente {req.ClientId}");
            return ok ? Ok(new { message = "Factura creada", invoiceNumber }) : BadRequest();
        }

        // GET /api/invoices/by-client/{clientId}
        [HttpGet("by-client/{clientId:int}")]
        public async Task<IActionResult> GetByClient(int clientId)
        {
            var dt = await _db.QueryAsync(
                "SELECT Id, InvoiceNumber, QuoteId, ClientId, IssueDate, DueDate, Subtotal, TotalTax, ReteFuente, Total, PaymentStatus FROM Invoices WHERE ClientId = @cId ORDER BY IssueDate DESC",
                new Dictionary<string, object> { ["@cId"] = clientId });

            return Ok(DbService.ToList(dt));
        }

        // GET /api/invoices/debt/{clientId}
        [HttpGet("debt/{clientId:int}")]
        public async Task<IActionResult> GetDebtByClient(int clientId)
        {
            const string sql = @"
                WITH Pay AS (
                    SELECT InvoiceId, SUM(Amount) AS Paid
                    FROM Payments
                    GROUP BY InvoiceId
                )
                SELECT i.Id, i.ClientId, i.IssueDate, i.DueDate, i.Total, i.PaymentStatus,
                       ISNULL(p.Paid,0) AS Paid,
                       i.Total - ISNULL(p.Paid,0) AS Pending,
                       CASE WHEN i.DueDate < GETDATE() AND (i.Total - ISNULL(p.Paid,0)) > 0 THEN 1 ELSE 0 END AS IsOverdue
                FROM Invoices i
                LEFT JOIN Pay p ON p.InvoiceId = i.Id
                WHERE i.ClientId = @clientId
                ORDER BY i.IssueDate DESC;";

            var dt = await _db.QueryAsync(sql, new Dictionary<string, object> { ["@clientId"] = clientId });
            if (dt.Rows.Count == 0) return Ok(new { clientId, pendingTotal = 0, overdueTotal = 0, invoices = new object[0] });

            decimal pendingTotal = 0;
            decimal overdueTotal = 0;
            foreach (System.Data.DataRow row in dt.Rows)
            {
                pendingTotal += Convert.ToDecimal(row["Pending"]);
                if (Convert.ToInt32(row["IsOverdue"]) == 1)
                    overdueTotal += Convert.ToDecimal(row["Pending"]);
            }

            return Ok(new
            {
                clientId,
                pendingTotal,
                overdueTotal,
                invoices = DbService.ToList(dt)
            });
        }

        // POST /api/invoices/from-quote/{quoteId}
        [HttpPost("from-quote/{quoteId:int}")]
        public async Task<IActionResult> CreateFromQuote(int quoteId)
        {
            var quote = await _db.QueryAsync("SELECT ClientId, TotalAmount FROM Quotes WHERE Id=@id",
                new Dictionary<string, object> { ["@id"] = quoteId });
            if (quote.Rows.Count == 0) return NotFound(new { error = "Cotizacion no encontrada" });

            var req = new CreateInvoiceRequest
            {
                QuoteId = quoteId,
                ClientId = Convert.ToInt32(quote.Rows[0]["ClientId"]),
                DueDate = DateTime.Now.AddDays(30),
                Subtotal = Convert.ToDecimal(quote.Rows[0]["TotalAmount"])
            };
            var resp = await Create(req);
            return resp;
        }

        // GET /api/invoices/{id}/pdf
        [HttpGet("{id:int}/pdf")]
        public async Task<IActionResult> GetPdf(int id)
        {
            var dt = await _db.QueryAsync(
                "SELECT i.InvoiceNumber, i.IssueDate, i.DueDate, i.Subtotal, i.TotalTax, i.ReteFuente, i.Total, c.Name, c.DocumentID, c.Address, c.City " +
                "FROM Invoices i JOIN Clients c ON c.Id = i.ClientId WHERE i.Id=@id",
                new Dictionary<string, object> { ["@id"] = id });
            if (dt.Rows.Count == 0) return NotFound();

            var row = dt.Rows[0];
            var pdfBytes = BuildSimplePdf(row);
            return File(pdfBytes, "application/pdf", $"Factura-{row["InvoiceNumber"]}.pdf");
        }

        private byte[] BuildSimplePdf(System.Data.DataRow row)
        {
            string text = $"Factura #{row["InvoiceNumber"]}\\n" +
                          $"Cliente: {row["Name"]} ({row["DocumentID"]})\\n" +
                          $"Direccion: {row["Address"]}, {row["City"]}\\n" +
                          $"Fecha: {Convert.ToDateTime(row["IssueDate"]).ToShortDateString()}\\n" +
                          $"Vence: {Convert.ToDateTime(row["DueDate"]).ToShortDateString()}\\n" +
                          $"Subtotal: {row["Subtotal"]}\\nIVA (19%): {row["TotalTax"]}\\n" +
                          $"ReteFuente: {row["ReteFuente"]}\\nTotal: {row["Total"]}";

            string[] lines = text.Split("\\n");
            var objects = new List<string>();
            objects.Add("1 0 obj << /Type /Catalog /Pages 2 0 R >> endobj\n");
            objects.Add("2 0 obj << /Type /Pages /Count 1 /Kids [3 0 R] >> endobj\n");
            objects.Add("3 0 obj << /Type /Page /Parent 2 0 R /MediaBox [0 0 612 792] /Contents 4 0 R /Resources << /Font << /F1 5 0 R >> >> >> endobj\n");

            var content = new StringBuilder();
            content.AppendLine("BT");
            content.AppendLine("/F1 12 Tf");
            int y = 750;
            foreach (var line in lines)
            {
                content.AppendLine($"1 0 0 1 50 {y} Tm ({EscapePdf(line)}) Tj");
                y -= 16;
            }
            content.AppendLine("ET");
            byte[] contentBytes = Encoding.ASCII.GetBytes(content.ToString());
            objects.Add($"4 0 obj << /Length {contentBytes.Length} >> stream\n{content}endstream endobj\n");
            objects.Add("5 0 obj << /Type /Font /Subtype /Type1 /BaseFont /Helvetica >> endobj\n");

            var sb = new StringBuilder();
            sb.Append("%PDF-1.4\n");
            var offsets = new List<int> { 0 }; // object 0 not used
            int cursor = sb.Length;
            foreach (var obj in objects)
            {
                offsets.Add(cursor);
                sb.Append(obj);
                cursor = sb.Length;
            }

            int xrefPos = sb.Length;
            sb.Append("xref\n");
            sb.Append($"0 {objects.Count + 1}\n");
            sb.Append("0000000000 65535 f \n");
            for (int i = 1; i <= objects.Count; i++)
            {
                sb.Append(offsets[i].ToString("D10")).Append(" 00000 n \n");
            }
            sb.Append("trailer << /Size ").Append(objects.Count + 1).Append(" /Root 1 0 R >>\n");
            sb.Append("startxref\n").Append(xrefPos).Append("\n%%EOF");

            return Encoding.ASCII.GetBytes(sb.ToString());
        }

        private string EscapePdf(string input) => input.Replace("\\", "\\\\").Replace("(", "\\(").Replace(")", "\\)");

        private async Task<int> NextInvoiceNumber()
        {
            var dt = await _db.QueryAsync("SELECT NEXT VALUE FOR InvoiceNumberSeq AS Num");
            if (dt.Rows.Count > 0) return Convert.ToInt32(dt.Rows[0]["Num"]);
            return new Random().Next(1000, 9999);
        }

        public class CreateInvoiceRequest
        {
            public int QuoteId { get; set; }
            public int ClientId { get; set; }
            public DateTime DueDate { get; set; }
            public decimal Subtotal { get; set; }
            public decimal? ReteFuentePct { get; set; }
        }
    }
}

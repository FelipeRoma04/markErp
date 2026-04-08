using Microsoft.AspNetCore.Mvc;
using Proyecto.Api.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Proyecto.Api.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly DbService _db;
        public ReportsController(DbService db) => _db = db;

        // GET /api/reports/payroll?from=2026-01-01&to=2026-01-31&format=csv|pdf
        [HttpGet("payroll")]
        public async Task<IActionResult> PayrollReport([FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null, [FromQuery] string format = "csv")
        {
            string where = "WHERE 1=1";
            if (from.HasValue) where += $" AND p.PayPeriodStart >= '{from:yyyy-MM-dd}'";
            if (to.HasValue) where += $" AND p.PayPeriodEnd <= '{to:yyyy-MM-dd}'";

            var dt = await _db.QueryAsync($@"
                SELECT p.Id, p.EmployeeId, e.Name, e.LastName, p.PayPeriodStart, p.PayPeriodEnd,
                       p.GrossPay, p.Deductions, p.NetPay, p.PaymentDate
                FROM Payroll_Log p
                LEFT JOIN Empleados e ON e.Id = p.EmployeeId
                {where}
                ORDER BY p.PaymentDate DESC");

            return format?.ToLowerInvariant() == "pdf"
                ? File(BuildSimplePdf(dt, "Reporte de Nómina"), "application/pdf", "payroll.pdf")
                : File(ToCsv(dt), "text/csv", "payroll.csv");
        }

        // GET /api/reports/sales-monthly?year=2026&month=1&format=csv|pdf
        [HttpGet("sales-monthly")]
        public async Task<IActionResult> SalesMonthly([FromQuery] int year, [FromQuery] int month, [FromQuery] string format = "csv")
        {
            if (year == 0 || month == 0) return BadRequest("year y month requeridos");
            var dt = await _db.QueryAsync(@"
                SELECT i.InvoiceNumber, i.IssueDate, i.ClientId, c.Name AS ClientName,
                       i.Subtotal, i.TotalTax, i.ReteFuente, i.Total, i.PaymentStatus
                FROM Invoices i
                LEFT JOIN Clients c ON c.Id = i.ClientId
                WHERE YEAR(i.IssueDate) = @y AND MONTH(i.IssueDate) = @m
                ORDER BY i.IssueDate",
                new Dictionary<string, object> { ["@y"] = year, ["@m"] = month });

            return format?.ToLowerInvariant() == "pdf"
                ? File(BuildSimplePdf(dt, $"Ventas {month}/{year}"), "application/pdf", $"ventas-{year}-{month}.pdf")
                : File(ToCsv(dt), "text/csv", $"ventas-{year}-{month}.csv");
        }

        // GET /api/reports/inventory?from=2026-01-01&to=2026-01-31&format=csv|pdf
        [HttpGet("inventory")]
        public async Task<IActionResult> Inventory([FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null, [FromQuery] string format = "csv")
        {
            var products = await _db.QueryAsync("SELECT Id, Code, Name, CostPrice, SalePrice, Stock, MinStock FROM Products ORDER BY Name");

            string where = "";
            if (from.HasValue || to.HasValue)
            {
                where = "WHERE 1=1";
                if (from.HasValue) where += $" AND MovementDate >= '{from:yyyy-MM-dd}'";
                if (to.HasValue) where += $" AND MovementDate <= '{to:yyyy-MM-dd}'";
            }

            var moves = await _db.QueryAsync($@"
                SELECT m.ProductId, p.Code, p.Name, m.MovementDate, m.Quantity, m.Type, m.Notes
                FROM StockMovements m
                JOIN Products p ON p.Id = m.ProductId
                {where}
                ORDER BY m.MovementDate DESC");

            // Merge into a single CSV/PDF by concatenating sections
            if (format?.ToLowerInvariant() == "pdf")
            {
                var combined = new DataTable("Inventory");
                combined.Columns.Add("Section");
                combined.Columns.Add("Data");
                combined.Rows.Add("Productos", "Ver CSV para detalle");
                combined.Rows.Add("Movimientos", "Ver CSV para detalle");
                return File(BuildSimplePdf(combined, "Inventario"), "application/pdf", "inventario.pdf");
            }

            var sb = new StringBuilder();
            sb.AppendLine("=== Productos ===");
            sb.Append(Encoding.UTF8.GetString(ToCsv(products)));
            sb.AppendLine();
            sb.AppendLine("=== Movimientos ===");
            sb.Append(Encoding.UTF8.GetString(ToCsv(moves)));
            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "inventario.csv");
        }

        // GET /api/dashboard/chart-data
        [HttpGet("/api/dashboard/chart-data")]
        public async Task<IActionResult> ChartData()
        {
            var sales = await _db.QueryAsync(@"
                SELECT FORMAT(IssueDate,'yyyy-MM') AS Period, SUM(Total) AS Total
                FROM Invoices
                GROUP BY FORMAT(IssueDate,'yyyy-MM')
                ORDER BY Period");

            var cxcs = await _db.QueryAsync(@"
                WITH pay AS (SELECT InvoiceId, SUM(Amount) AS Paid FROM Payments GROUP BY InvoiceId)
                SELECT PaymentStatus, COUNT(*) AS CountInvoices
                FROM Invoices i
                LEFT JOIN pay p ON p.InvoiceId = i.Id
                GROUP BY PaymentStatus");

            var byProduct = await _db.QueryAsync("SELECT Name, Stock FROM Products ORDER BY Stock DESC");

            return Ok(new
            {
                sales = DbService.ToList(sales),
                receivables = DbService.ToList(cxcs),
                products = DbService.ToList(byProduct)
            });
        }

        private byte[] ToCsv(DataTable dt)
        {
            var sb = new StringBuilder();
            var cols = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
            sb.AppendLine(string.Join(",", cols));
            foreach (DataRow row in dt.Rows)
            {
                var vals = dt.Columns.Cast<DataColumn>().Select(c => EscapeCsv(row[c]));
                sb.AppendLine(string.Join(",", vals));
            }
            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        private string EscapeCsv(object val)
        {
            if (val == null || val == DBNull.Value) return "";
            var s = val.ToString();
            if (s.Contains(",") || s.Contains("\"") || s.Contains("\n"))
            {
                s = s.Replace("\"", "\"\"");
                return $"\"{s}\"";
            }
            return s;
        }

        private byte[] BuildSimplePdf(DataTable dt, string title)
        {
            var lines = new List<string> { title };
            lines.Add(string.Join(" | ", dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));
            foreach (DataRow r in dt.Rows)
                lines.Add(string.Join(" | ", r.ItemArray.Select(o => o?.ToString() ?? "")));

            var objects = new List<string>();
            objects.Add("1 0 obj << /Type /Catalog /Pages 2 0 R >> endobj\n");
            objects.Add("2 0 obj << /Type /Pages /Count 1 /Kids [3 0 R] >> endobj\n");
            objects.Add("3 0 obj << /Type /Page /Parent 2 0 R /MediaBox [0 0 612 792] /Contents 4 0 R /Resources << /Font << /F1 5 0 R >> >> >> endobj\n");

            var content = new StringBuilder();
            content.AppendLine("BT");
            content.AppendLine("/F1 10 Tf");
            int y = 760;
            foreach (var line in lines)
            {
                content.AppendLine($"1 0 0 1 40 {y} Tm ({EscapePdf(line)}) Tj");
                y -= 14;
                if (y < 40) break;
            }
            content.AppendLine("ET");
            byte[] contentBytes = Encoding.ASCII.GetBytes(content.ToString());
            objects.Add($"4 0 obj << /Length {contentBytes.Length} >> stream\n{content}endstream endobj\n");
            objects.Add("5 0 obj << /Type /Font /Subtype /Type1 /BaseFont /Helvetica >> endobj\n");

            var sb = new StringBuilder();
            sb.Append("%PDF-1.4\n");
            var offsets = new List<int> { 0 };
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

        private string EscapePdf(string input) => (input ?? string.Empty).Replace("\\", "\\\\").Replace("(", "\\(").Replace(")", "\\)");
    }
}

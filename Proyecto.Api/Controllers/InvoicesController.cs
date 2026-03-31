using Microsoft.AspNetCore.Mvc;
using Proyecto.Api.Data;
using System.Collections.Generic;

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
                $"SELECT * FROM Invoices {where} ORDER BY Id DESC", p);
            return Ok(DbService.ToList(dt));
        }

        // GET /api/invoices/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dt = await _db.QueryAsync(
                "SELECT * FROM Invoices WHERE Id = @id",
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
            decimal total = req.Subtotal + tax;

            bool ok = await _db.ExecuteAsync(
                "INSERT INTO Invoices (QuoteId, ClientId, IssueDate, DueDate, Subtotal, TotalTax, Total) " +
                "VALUES (@qId, @cId, GETDATE(), @due, @sub, @tax, @total)",
                new Dictionary<string, object>
                {
                    ["@qId"] = req.QuoteId,
                    ["@cId"] = req.ClientId,
                    ["@due"] = req.DueDate,
                    ["@sub"] = req.Subtotal,
                    ["@tax"] = tax,
                    ["@total"] = total
                });

            return ok ? Ok(new { message = "Factura creada" }) : BadRequest();
        }

        // GET /api/invoices/by-client/{clientId}
        [HttpGet("by-client/{clientId:int}")]
        public async Task<IActionResult> GetByClient(int clientId)
        {
            var dt = await _db.QueryAsync(
                "SELECT * FROM Invoices WHERE ClientId = @cId ORDER BY IssueDate DESC",
                new Dictionary<string, object> { ["@cId"] = clientId });

            return Ok(DbService.ToList(dt));
        }

        public class CreateInvoiceRequest
        {
            public int QuoteId { get; set; }
            public int ClientId { get; set; }
            public DateTime DueDate { get; set; }
            public decimal Subtotal { get; set; }
        }
    }
}

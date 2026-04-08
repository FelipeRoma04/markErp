using Microsoft.AspNetCore.Mvc;
using Proyecto.Api.Data;
using System.Collections.Generic;

namespace Proyecto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotesController : ControllerBase
    {
        private readonly DbService _db;

        public QuotesController(DbService db) => _db = db;

        // GET /api/quotes
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? clientId = null)
        {
            string where = "WHERE 1=1";
            var p = new Dictionary<string, object>();

            if (clientId.HasValue)
            {
                where += " AND ClientId = @cId";
                p["@cId"] = clientId;
            }

            var dt = await _db.QueryAsync(
                $"SELECT Id, ClientId, IssueDate, ExpirationDate, TotalAmount, Status FROM Quotes {where} ORDER BY IssueDate DESC", p);
            return Ok(DbService.ToList(dt));
        }

        // GET /api/quotes/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dt = await _db.QueryAsync(
                "SELECT Id, ClientId, IssueDate, ExpirationDate, TotalAmount, Status FROM Quotes WHERE Id = @id",
                new Dictionary<string, object> { ["@id"] = id });

            if (dt.Rows.Count == 0) return NotFound();
            return Ok(DbService.ToList(dt)[0]);
        }

        // POST /api/quotes
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateQuoteRequest req)
        {
            if (req.ClientId <= 0 || req.Total <= 0)
                return BadRequest("ClientId y Total requeridos.");

            bool ok = await _db.ExecuteAsync(
                "INSERT INTO Quotes (ClientId, IssueDate, ExpirationDate, TotalAmount, Status) " +
                "VALUES (@cId, GETDATE(), @exp, @total, @status)",
                new Dictionary<string, object>
                {
                    ["@cId"] = req.ClientId,
                    ["@exp"] = req.ExpirationDate,
                    ["@total"] = req.Total,
                    ["@status"] = string.IsNullOrWhiteSpace(req.Status) ? "Pendiente" : req.Status
                });

            return ok ? Ok(new { message = "Cotización creada" }) : BadRequest();
        }

        // PUT /api/quotes/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateQuoteRequest req)
        {
            bool ok = await _db.ExecuteAsync(
                "UPDATE Quotes SET ClientId=@cId, ExpirationDate=@exp, TotalAmount=@total, Status=@status WHERE Id=@id",
                new Dictionary<string, object>
                {
                    ["@id"] = id,
                    ["@cId"] = req.ClientId,
                    ["@exp"] = req.ExpirationDate,
                    ["@total"] = req.Total,
                    ["@status"] = string.IsNullOrWhiteSpace(req.Status) ? "Pendiente" : req.Status
                });

            return ok ? Ok(new { message = "Cotización actualizada" }) : BadRequest();
        }

        // DELETE /api/quotes/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool ok = await _db.ExecuteAsync(
                "DELETE FROM Quotes WHERE Id=@id",
                new Dictionary<string, object> { ["@id"] = id });

            return ok ? Ok(new { message = "Cotización eliminada" }) : BadRequest();
        }

        public class CreateQuoteRequest
        {
            public int ClientId { get; set; }
            public DateTime ExpirationDate { get; set; }
            public decimal Total { get; set; }
            public string Status { get; set; }
        }
    }
}

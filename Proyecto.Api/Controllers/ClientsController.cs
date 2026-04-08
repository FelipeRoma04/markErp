using Microsoft.AspNetCore.Mvc;
using Proyecto.Api.Data;
using Proyecto.Api.Services;
using System.Collections.Generic;

namespace Proyecto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly DbService _db;

        public ClientsController(DbService db) => _db = db;

        // GET /api/clients
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dt = await _db.QueryAsync("SELECT Id, DocumentID, Name, Email, Phone, Address, City FROM Clients ORDER BY Id DESC", new());
            return Ok(DbService.ToList(dt));
        }

        // GET /api/clients/search?q=...
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string q)
        {
            q = q ?? "";
            var dt = await _db.QueryAsync(
                "SELECT TOP 50 Id, DocumentID, Name, Email, Phone, Address, City FROM Clients " +
                "WHERE DocumentID LIKE @q OR Name LIKE @q OR City LIKE @q " +
                "ORDER BY Name",
                new Dictionary<string, object> { ["@q"] = "%" + q + "%" });
            return Ok(DbService.ToList(dt));
        }

        // GET /api/clients/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dt = await _db.QueryAsync(
                "SELECT Id, DocumentID, Name, Email, Phone, Address, City FROM Clients WHERE Id = @id",
                new Dictionary<string, object> { ["@id"] = id });

            if (dt.Rows.Count == 0) return NotFound();
            return Ok(DbService.ToList(dt)[0]);
        }

        // POST /api/clients
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClientRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.DocumentId) || string.IsNullOrWhiteSpace(req.Name))
                return BadRequest("DocumentId y Name requeridos.");

            bool ok = await _db.ExecuteAsync(
                "INSERT INTO Clients (DocumentID, Name, Email, Phone, Address, City) " +
                "VALUES (@doc, @name, @email, @phone, @address, @city)",
                new Dictionary<string, object>
                {
                    ["@doc"] = req.DocumentId,
                    ["@name"] = req.Name,
                    ["@email"] = req.Email ?? "",
                    ["@phone"] = req.Phone ?? "",
                    ["@address"] = req.Address ?? "",
                    ["@city"] = req.City ?? ""
                });

            if (ok) await AuditHelper.LogAsync(_db, req.User ?? "api", "CREATE", "Clients", req.DocumentId, req.Name);

            return ok ? Ok(new { message = "Cliente creado" }) : BadRequest();
        }

        // PUT /api/clients/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateClientRequest req)
        {
            bool ok = await _db.ExecuteAsync(
                "UPDATE Clients SET DocumentID=@doc, Name=@name, Email=@email, Phone=@phone, Address=@address, City=@city WHERE Id=@id",
                new Dictionary<string, object>
                {
                    ["@id"] = id,
                    ["@doc"] = req.DocumentId,
                    ["@name"] = req.Name,
                    ["@email"] = req.Email ?? "",
                    ["@phone"] = req.Phone ?? "",
                    ["@address"] = req.Address ?? "",
                    ["@city"] = req.City ?? ""
                });

            if (ok) await AuditHelper.LogAsync(_db, req.User ?? "api", "UPDATE", "Clients", id.ToString(), req.Name);

            return ok ? Ok(new { message = "Cliente actualizado" }) : BadRequest();
        }

        // DELETE /api/clients/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool ok = await _db.ExecuteAsync(
                "DELETE FROM Clients WHERE Id=@id",
                new Dictionary<string, object> { ["@id"] = id });

            if (ok) await AuditHelper.LogAsync(_db, "api", "DELETE", "Clients", id.ToString(), null);

            return ok ? Ok(new { message = "Cliente eliminado" }) : BadRequest();
        }

        public class CreateClientRequest
        {
            public string DocumentId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string User { get; set; }
        }
    }
}

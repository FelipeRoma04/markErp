using Microsoft.AspNetCore.Mvc;
using Proyecto.Api.Data;
using System;
using System.Collections.Generic;

namespace Proyecto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly DbService _db;

        public AssetsController(DbService db) => _db = db;

        // GET /api/assets
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? type = null, [FromQuery] string? status = null)
        {
            string where = "WHERE 1=1";
            var p = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(type))   { where += " AND Type = @type";     p["@type"]   = type; }
            if (!string.IsNullOrEmpty(status)) { where += " AND Status = @status"; p["@status"] = status; }

            var dt = await _db.QueryAsync(
                $"SELECT Id, SerialNumber, Type, Brand, Status, Location, PurchaseDate, PurchaseValue, DepreciationMonths, ResidualValue " +
                $"FROM Assets {where} ORDER BY Id", p);

            return Ok(DbService.ToList(dt));
        }

        // GET /api/assets/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dt = await _db.QueryAsync(
                "SELECT * FROM Assets WHERE Id = @id",
                new Dictionary<string, object> { ["@id"] = id });

            if (dt.Rows.Count == 0) return NotFound(new { error = $"Activo con Id {id} no encontrado." });
            return Ok(DbService.ToList(dt)[0]);
        }

        // POST /api/assets
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAssetRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.SerialNumber) || string.IsNullOrWhiteSpace(req.Type))
                return BadRequest(new { error = "SerialNumber y Type son obligatorios." });

            bool ok = await _db.ExecuteAsync(
                "INSERT INTO Assets (SerialNumber, Type, Brand, Status, Location, PurchaseDate, PurchaseValue, DepreciationMonths, ResidualValue) " +
                "VALUES (@serial, @type, @brand, 'Available', @loc, @date, @value, @dep, @residual)",
                new Dictionary<string, object>
                {
                    ["@serial"]  = req.SerialNumber,
                    ["@type"]    = req.Type,
                    ["@brand"]   = req.Brand ?? "",
                    ["@loc"]     = req.Location ?? "",
                    ["@date"]    = req.PurchaseDate == default ? DateTime.Today : req.PurchaseDate,
                    ["@value"]   = req.PurchaseValue,
                    ["@dep"]     = req.DepreciationMonths,
                    ["@residual"]= req.ResidualValue
                });

            if (!ok) return StatusCode(500, new { error = "Error al registrar activo en base de datos." });
            return Created($"/api/assets", new { message = "Activo registrado exitosamente.", serial = req.SerialNumber });
        }

        // GET /api/assets/{id}/history
        [HttpGet("{id:int}/history")]
        public async Task<IActionResult> GetHistory(int id)
        {
            var dt = await _db.QueryAsync(
                "SELECT * FROM AssetAssignments WHERE AssetId = @id ORDER BY AssignDate DESC",
                new Dictionary<string, object> { ["@id"] = id });
            return Ok(DbService.ToList(dt));
        }
    }

    public record CreateAssetRequest(
        string SerialNumber, string Type, string? Brand,
        string? Location, DateTime PurchaseDate,
        decimal PurchaseValue, int DepreciationMonths, decimal ResidualValue);
}

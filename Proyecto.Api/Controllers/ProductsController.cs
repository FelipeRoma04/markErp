using Microsoft.AspNetCore.Mvc;
using Proyecto.Api.Data;
using Proyecto.Api.Services;
using System.Collections.Generic;

namespace Proyecto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly DbService _db;
        public ProductsController(DbService db) => _db = db;

        // GET /api/products
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool? lowStock = null)
        {
            string where = lowStock == true ? "WHERE Stock <= MinStock" : "";
            var dt = await _db.QueryAsync($"SELECT Id, Code, Name, CostPrice, SalePrice, Stock, MinStock FROM Products {where} ORDER BY Name");
            return Ok(DbService.ToList(dt));
        }

        // GET /api/products/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dt = await _db.QueryAsync("SELECT Id, Code, Name, CostPrice, SalePrice, Stock, MinStock FROM Products WHERE Id = @id",
                new Dictionary<string, object> { ["@id"] = id });
            if (dt.Rows.Count == 0) return NotFound(new { error = $"Producto con Id {id} no encontrado." });
            return Ok(DbService.ToList(dt)[0]);
        }

        // POST /api/products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Code) || string.IsNullOrWhiteSpace(req.Name))
                return BadRequest("Code y Name requeridos.");

            bool ok = await _db.ExecuteAsync(
                "INSERT INTO Products (Code, Name, CostPrice, SalePrice, Stock, MinStock) VALUES (@code, @name, @cost, @sale, @stock, @min)",
                new Dictionary<string, object>
                {
                    ["@code"] = req.Code,
                    ["@name"] = req.Name,
                    ["@cost"] = req.CostPrice,
                    ["@sale"] = req.SalePrice,
                    ["@stock"] = req.Stock,
                    ["@min"] = req.MinStock
                });
            if (ok) await AuditHelper.LogAsync(_db, req.User ?? "api", "CREATE", "Products", req.Code, req.Name);
            return ok ? Ok(new { message = "Producto creado" }) : BadRequest();
        }

        // PUT /api/products/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateProductRequest req)
        {
            bool ok = await _db.ExecuteAsync(
                "UPDATE Products SET Code=@code, Name=@name, CostPrice=@cost, SalePrice=@sale, Stock=@stock, MinStock=@min WHERE Id=@id",
                new Dictionary<string, object>
                {
                    ["@id"] = id,
                    ["@code"] = req.Code,
                    ["@name"] = req.Name,
                    ["@cost"] = req.CostPrice,
                    ["@sale"] = req.SalePrice,
                    ["@stock"] = req.Stock,
                    ["@min"] = req.MinStock
                });
            if (ok) await AuditHelper.LogAsync(_db, req.User ?? "api", "UPDATE", "Products", id.ToString(), req.Name);
            return ok ? Ok(new { message = "Producto actualizado" }) : BadRequest();
        }

        // DELETE /api/products/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] string user = "api")
        {
            bool ok = await _db.ExecuteAsync("DELETE FROM Products WHERE Id=@id", new Dictionary<string, object> { ["@id"] = id });
            if (ok) await AuditHelper.LogAsync(_db, user, "DELETE", "Products", id.ToString(), null);
            return ok ? Ok(new { message = "Producto eliminado" }) : BadRequest();
        }

        // POST /api/products/{id}/stock
        [HttpPost("{id:int}/stock")]
        public async Task<IActionResult> AdjustStock(int id, [FromBody] StockMovementRequest req)
        {
            if (req.Quantity == 0) return BadRequest("Quantity debe ser distinto de cero.");
            string type = req.Quantity > 0 ? "IN" : "OUT";

            bool ok = await _db.ExecuteAsync(
                "INSERT INTO StockMovements (ProductId, Quantity, Type, Notes) VALUES (@pid, @qty, @type, @notes)",
                new Dictionary<string, object>
                {
                    ["@pid"] = id,
                    ["@qty"] = req.Quantity,
                    ["@type"] = type,
                    ["@notes"] = req.Notes ?? ""
                });
            if (ok)
            {
                await _db.ExecuteAsync("UPDATE Products SET Stock = Stock + @qty WHERE Id=@pid",
                    new Dictionary<string, object> { ["@qty"] = req.Quantity, ["@pid"] = id });
                await AuditHelper.LogAsync(_db, req.User ?? "api", "STOCK_" + type, "Products", id.ToString(), req.Notes);
            }
            return ok ? Ok(new { message = "Stock actualizado" }) : BadRequest();
        }

        public class CreateProductRequest
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public decimal CostPrice { get; set; }
            public decimal SalePrice { get; set; }
            public int Stock { get; set; }
            public int MinStock { get; set; }
            public string User { get; set; }
        }

        public class StockMovementRequest
        {
            public int Quantity { get; set; }
            public string Notes { get; set; }
            public string User { get; set; }
        }
    }

    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok(new { status = "ok", timestamp = DateTime.UtcNow });
    }
}

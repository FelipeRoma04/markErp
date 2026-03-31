using Microsoft.AspNetCore.Mvc;
using Proyecto.Api.Data;

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
            var dt = await _db.QueryAsync($"SELECT * FROM Products {where} ORDER BY Name");
            return Ok(DbService.ToList(dt));
        }

        // GET /api/products/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dt = await _db.QueryAsync("SELECT * FROM Products WHERE Id = @id",
                new Dictionary<string, object> { ["@id"] = id });
            if (dt.Rows.Count == 0) return NotFound(new { error = $"Producto con Id {id} no encontrado." });
            return Ok(DbService.ToList(dt)[0]);
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

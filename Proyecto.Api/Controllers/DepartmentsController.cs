using Microsoft.AspNetCore.Mvc;
using Proyecto.Api.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly DbService _db;

        public DepartmentsController(DbService db) => _db = db;

        // GET /api/departments
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dt = await _db.QueryAsync("SELECT * FROM Departamentos ORDER BY departamento", new());
            return Ok(DbService.ToList(dt));
        }

        // GET /api/departments/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dt = await _db.QueryAsync(
                "SELECT * FROM Departamentos WHERE Id = @id",
                new Dictionary<string, object> { ["@id"] = id });

            if (dt.Rows.Count == 0) return NotFound();
            return Ok(DbService.ToList(dt)[0]);
        }

        // POST /api/departments
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Name))
                return BadRequest("Name es requerido.");

            bool ok = await _db.ExecuteAsync(
                "INSERT INTO Departamentos (departamento) VALUES (@name)",
                new Dictionary<string, object> { ["@name"] = req.Name });

            return ok ? Ok(new { message = "Departamento creado" }) : BadRequest();
        }

        // PUT /api/departments/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateDepartmentRequest req)
        {
            bool ok = await _db.ExecuteAsync(
                "UPDATE Departamentos SET departamento=@name WHERE Id=@id",
                new Dictionary<string, object> { ["@id"] = id, ["@name"] = req.Name });

            return ok ? Ok(new { message = "Departamento actualizado" }) : BadRequest();
        }

        // DELETE /api/departments/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool ok = await _db.ExecuteAsync(
                "DELETE FROM Departamentos WHERE Id=@id",
                new Dictionary<string, object> { ["@id"] = id });

            return ok ? Ok(new { message = "Departamento eliminado" }) : BadRequest();
        }

        public class CreateDepartmentRequest
        {
            public string Name { get; set; }
        }
    }
}

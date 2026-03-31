using Microsoft.AspNetCore.Mvc;
using Proyecto.Api.Data;

namespace Proyecto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly DbService _db;
        public EmployeesController(DbService db) => _db = db;

        // GET /api/employees
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? departmentId = null)
        {
            string where = departmentId.HasValue ? "WHERE e.DepartmentId = @deptId" : "";
            var p = departmentId.HasValue
                ? new Dictionary<string, object> { ["@deptId"] = departmentId.Value }
                : null;

            var dt = await _db.QueryAsync(
                $"SELECT e.Id, e.Name, e.LastName, e.SecondName, e.Email, d.departamento AS Department " +
                $"FROM Empleados e LEFT JOIN Departamentos d ON e.DepartmentId = d.Id {where} ORDER BY e.LastName, e.Name", p);

            return Ok(DbService.ToList(dt));
        }

        // GET /api/employees/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dt = await _db.QueryAsync(
                "SELECT e.Id, e.Name, e.LastName, e.SecondName, e.Email, d.departamento AS Department " +
                "FROM Empleados e LEFT JOIN Departamentos d ON e.DepartmentId = d.Id WHERE e.Id = @id",
                new Dictionary<string, object> { ["@id"] = id });

            if (dt.Rows.Count == 0) return NotFound(new { error = $"Empleado con Id {id} no encontrado." });
            return Ok(DbService.ToList(dt)[0]);
        }
    }
}

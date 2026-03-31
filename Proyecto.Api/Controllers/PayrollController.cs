using Microsoft.AspNetCore.Mvc;
using Proyecto.Api.Data;
using System.Collections.Generic;

namespace Proyecto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollController : ControllerBase
    {
        private readonly DbService _db;

        public PayrollController(DbService db) => _db = db;

        // GET /api/payroll
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? employeeId = null)
        {
            string where = "WHERE 1=1";
            var p = new Dictionary<string, object>();
            if (employeeId.HasValue)
            {
                where += " AND EmployeeId = @empId";
                p["@empId"] = employeeId;
            }

            var dt = await _db.QueryAsync(
                $"SELECT * FROM Payroll_Log {where} ORDER BY Id DESC", p);
            return Ok(DbService.ToList(dt));
        }

        // GET /api/payroll/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dt = await _db.QueryAsync(
                "SELECT * FROM Payroll_Log WHERE Id = @id",
                new Dictionary<string, object> { ["@id"] = id });

            if (dt.Rows.Count == 0) return NotFound();
            return Ok(DbService.ToList(dt)[0]);
        }

        // POST /api/payroll
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePayrollRequest req)
        {
            if (req.EmployeeId <= 0 || req.GrossPay <= 0)
                return BadRequest("EmployeeId y GrossPay requeridos.");

            decimal deductions = req.GrossPay * 0.08m;
            decimal netPay = req.GrossPay - deductions;

            bool ok = await _db.ExecuteAsync(
                "INSERT INTO Payroll_Log (EmployeeId, PayPeriodStart, PayPeriodEnd, GrossPay, Deductions, NetPay, PaymentDate) " +
                "VALUES (@empId, @pStart, @pEnd, @gross, @ded, @net, GETDATE())",
                new Dictionary<string, object>
                {
                    ["@empId"] = req.EmployeeId,
                    ["@pStart"] = req.PayPeriodStart,
                    ["@pEnd"] = req.PayPeriodEnd,
                    ["@gross"] = req.GrossPay,
                    ["@ded"] = deductions,
                    ["@net"] = netPay
                });

            return ok ? Ok(new { message = "Nómina creada" }) : BadRequest();
        }

        public class CreatePayrollRequest
        {
            public int EmployeeId { get; set; }
            public DateTime PayPeriodStart { get; set; }
            public DateTime PayPeriodEnd { get; set; }
            public decimal GrossPay { get; set; }
        }
    }
}

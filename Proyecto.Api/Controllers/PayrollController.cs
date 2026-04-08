using Microsoft.AspNetCore.Mvc;
using Proyecto.Api.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

            var breakdown = CalculateLegalDeductions(req.GrossPay);
            decimal totalDeductions = breakdown.TotalDeductions;
            decimal netPay = req.GrossPay - totalDeductions;

            bool ok = await _db.ExecuteAsync(
                "INSERT INTO Payroll_Log (EmployeeId, PayPeriodStart, PayPeriodEnd, GrossPay, Deductions, NetPay, PaymentDate) " +
                "VALUES (@empId, @pStart, @pEnd, @gross, @ded, @net, @payDate)",
                new Dictionary<string, object>
                {
                    ["@empId"] = req.EmployeeId,
                    ["@pStart"] = req.PayPeriodStart,
                    ["@pEnd"] = req.PayPeriodEnd,
                    ["@gross"] = req.GrossPay,
                    ["@ded"] = totalDeductions,
                    ["@net"] = netPay,
                    ["@payDate"] = req.PaymentDate == default ? DateTime.Now : req.PaymentDate
                });

            return ok
                ? Ok(new
                {
                    message = "Nomina creada",
                    netPay,
                    breakdown
                })
                : BadRequest();
        }

        public class CreatePayrollRequest
        {
            public int EmployeeId { get; set; }
            public DateTime PayPeriodStart { get; set; }
            public DateTime PayPeriodEnd { get; set; }
            public decimal GrossPay { get; set; }
            public DateTime PaymentDate { get; set; }
        }

        private static PayrollBreakdown CalculateLegalDeductions(decimal gross)
        {
            decimal salud = Math.Round(gross * 0.04m, 2);
            decimal pension = Math.Round(gross * 0.04m, 2);
            decimal parafiscales = Math.Round(gross * 0.09m, 2);      // Aportes empresariales aproximados
            decimal cesantias = Math.Round(gross * 0.0833m, 2);
            decimal prima = Math.Round(gross * 0.0833m, 2);
            decimal vacaciones = Math.Round(gross * 0.0417m, 2);

            decimal total = salud + pension + parafiscales + cesantias + prima + vacaciones;
            return new PayrollBreakdown
            {
                Salud = salud,
                Pension = pension,
                Parafiscales = parafiscales,
                Cesantias = cesantias,
                PrimaServicios = prima,
                Vacaciones = vacaciones,
                TotalDeductions = total
            };
        }

        public class PayrollBreakdown
        {
            public decimal Salud { get; set; }
            public decimal Pension { get; set; }
            public decimal Parafiscales { get; set; }
            public decimal Cesantias { get; set; }
            public decimal PrimaServicios { get; set; }
            public decimal Vacaciones { get; set; }
            public decimal TotalDeductions { get; set; }
        }
    }
}

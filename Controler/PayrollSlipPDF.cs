using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using Proyecto.Model;

namespace Proyecto.Controler
{
    /// <summary>
    /// Task 25: PDF Payroll Slip Generator
    /// Generates professional payroll slips with Colombian deductions breakdown
    /// </summary>
    public static class PayrollSlipPDF
    {
        public class PayslipData
        {
            public string EmployeeName { get; set; }
            public string EmployeePosition { get; set; }
            public string EmployeeDocument { get; set; }
            public DateTime PayPeriodStart { get; set; }
            public DateTime PayPeriodEnd { get; set; }
            public decimal GrossPay { get; set; }
            public decimal SaludDeduction { get; set; }
            public decimal PensionDeduction { get; set; }
            public decimal FondoSolidaridad { get; set; }
            public decimal Parafiscales { get; set; }
            public decimal Cesantias { get; set; }
            public decimal PrimaServicios { get; set; }
            public decimal Vacaciones { get; set; }
            public decimal TotalDeductions { get; set; }
            public decimal NetPay { get; set; }
            public DateTime PaymentDate { get; set; }
            public int PayrollLogId { get; set; }
        }

        /// <summary>
        /// Task 25: Generate professional payroll slip PDF
        /// </summary>
        public static bool GeneratePayrollSlipPDF(int payrollLogId, PayslipData data)
        {
            try
            {
                var coCulture = new System.Globalization.CultureInfo("es-CO");
                string path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"Nomina_{data.EmployeeName.Replace(" ", "_")}_{data.PayPeriodEnd:yyyyMMdd}_{DateTime.Now:HHmmss}.html");

                string grossFormatted = data.GrossPay.ToString("C", coCulture);
                string saludFormatted = data.SaludDeduction.ToString("C", coCulture);
                string pensionFormatted = data.PensionDeduction.ToString("C", coCulture);
                string fondoFormatted = data.FondoSolidaridad.ToString("C", coCulture);
                string parafiscalesFormatted = data.Parafiscales.ToString("C", coCulture);
                string cesantiasFormatted = data.Cesantias.ToString("C", coCulture);
                string primaFormatted = data.PrimaServicios.ToString("C", coCulture);
                string vacacionesFormatted = data.Vacaciones.ToString("C", coCulture);
                string totalDedFormatted = data.TotalDeductions.ToString("C", coCulture);
                string netFormatted = data.NetPay.ToString("C", coCulture);

                string html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Comprobante de Nómina</title>
    <style>
        body {{ font-family: 'Courier New', monospace; padding: 20px; background-color: #f9f9f9; }}
        .container {{ max-width: 900px; margin: 0 auto; background-color: #fff; border: 2px solid #333; padding: 40px; box-shadow: 0 0 10px rgba(0,0,0,0.1); }}
        .header {{ text-align: center; border-bottom: 3px double #333; padding-bottom: 20px; margin-bottom: 30px; }}
        .header h1 {{ margin: 0; font-size: 28px; color: #1a1a1a; }}
        .header p {{ margin: 5px 0; color: #555; font-size: 14px; }}
        .company-name {{ font-size: 18px; font-weight: bold; color: #d9534f; margin: 10px 0; }}
        .employee-info {{ display: flex; justify-content: space-between; margin-bottom: 25px; }}
        .info-block {{ flex: 1; margin-right: 20px; }}
        .info-block h4 {{ margin: 0 0 10px 0; color: #333; font-size: 12px; text-transform: uppercase; border-bottom: 1px solid #ddd; padding-bottom: 5px; }}
        .info-block p {{ margin: 5px 0; font-size: 13px; }}
        .period-block {{ text-align: right; flex: 1; }}
        table {{ width: 100%; border-collapse: collapse; margin: 20px 0; font-size: 13px; }}
        table.deductions-table {{ margin-top: 30px; }}
        th {{ background-color: #4a4a4a; color: #fff; padding: 12px; text-align: left; font-weight: bold; border: 1px solid #333; }}
        td {{ padding: 10px; border: 1px solid #ddd; }}
        tr:nth-child(even) {{ background-color: #f5f5f5; }}
        .text-right {{ text-align: right; }}
        .bold {{ font-weight: bold; }}
        .deduction-row {{ display: flex; justify-content: space-between; padding: 8px 0; border-bottom: 1px solid #ddd; }}
        .deduction-label {{ flex: 1; }}
        .deduction-value {{ width: 150px; text-align: right; }}
        .subtotals {{ margin-top: 15px; padding: 10px; background-color: #f0f0f0; border: 1px solid #ddd; }}
        .subtotal-row {{ display: flex; justify-content: space-between; margin: 8px 0; font-size: 14px; }}
        .net-pay {{ display: flex; justify-content: space-between; margin-top: 20px; padding: 15px; background-color: #fff3cd; border: 2px solid #ffc107; font-size: 16px; font-weight: bold; }}
        .net-label {{ color: #333; }}
        .net-value {{ color: #d9534f; }}
        .footer {{ text-align: center; margin-top: 40px; padding-top: 20px; border-top: 1px solid #ddd; color: #666; font-size: 11px; }}
        .footer p {{ margin: 5px 0; }}
        .signature-area {{ display: flex; justify-content: space-around; margin-top: 50px; text-align: center; font-size: 12px; }}
        .signature-line {{ width: 200px; padding-top: 30px; border-top: 1px solid #333; }}
        .alert-row {{ background-color: #fff3cd; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <div class='company-name'>EMPRESA - SISTEMA DE NÓMINA</div>
            <h1>COMPROBANTE DE NÓMINA</h1>
            <p>Período: {data.PayPeriodStart:dd/MMMM/yyyy} al {data.PayPeriodEnd:dd/MMMM/yyyy}</p>
        </div>

        <div class='employee-info'>
            <div class='info-block'>
                <h4>Datos del Empleado</h4>
                <p><strong>Nombre:</strong> {data.EmployeeName}</p>
                <p><strong>Cédula:</strong> {data.EmployeeDocument}</p>
                <p><strong>Cargo:</strong> {data.EmployeePosition}</p>
            </div>
            <div class='period-block'>
                <h4>Fecha de Generación</h4>
                <p>{DateTime.Now:dd/MMMM/yyyy HH:mm:ss}</p>
            </div>
        </div>

        <table>
            <tr>
                <th>CONCEPTO</th>
                <th class='text-right'>VALOR</th>
            </tr>
            <tr>
                <td><strong>SALARIO BRUTO</strong></td>
                <td class='text-right bold'>{grossFormatted}</td>
            </tr>
        </table>

        <h3 style='margin-top: 30px; color: #333; border-bottom: 2px solid #d9534f; padding-bottom: 10px;'>DEDUCCIONES</h3>

        <div class='deductions-table'>
            <div class='deduction-row'>
                <div class='deduction-label'>Aporte Salud (4%)</div>
                <div class='deduction-value'>{saludFormatted}</div>
            </div>
            <div class='deduction-row'>
                <div class='deduction-label'>Aporte Pensión (4%)</div>
                <div class='deduction-value'>{pensionFormatted}</div>
            </div>";

            // Conditional: Fondo Solidaridad only if applied
            if (data.FondoSolidaridad > 0)
            {
                html += $@"
            <div class='deduction-row alert-row'>
                <div class='deduction-label'>Fondo Solidaridad (1% por ingresos > 4 SMLV)</div>
                <div class='deduction-value'>{fondoFormatted}</div>
            </div>";
            }

            html += $@"
            <div class='deduction-row'>
                <div class='deduction-label'>Parafiscales (9%)</div>
                <div class='deduction-value'>{parafiscalesFormatted}</div>
            </div>
            <div class='deduction-row'>
                <div class='deduction-label'>Cesantías (8.33%)</div>
                <div class='deduction-value'>{cesantiasFormatted}</div>
            </div>
            <div class='deduction-row'>
                <div class='deduction-label'>Prima de Servicios (8.33%)</div>
                <div class='deduction-value'>{primaFormatted}</div>
            </div>
            <div class='deduction-row'>
                <div class='deduction-label'>Vacaciones (4.17%)</div>
                <div class='deduction-value'>{vacacionesFormatted}</div>
            </div>
        </div>

        <div class='subtotals'>
            <div class='subtotal-row'>
                <span>TOTAL DEDUCCIONES:</span>
                <span class='bold'>{totalDedFormatted}</span>
            </div>
        </div>

        <div class='net-pay'>
            <span class='net-label'>SALARIO NETO A PAGAR:</span>
            <span class='net-value'>{netFormatted}</span>
        </div>

        <div class='footer'>
            <p><strong>Fecha de Pago:</strong> {data.PaymentDate:dd/MMMM/yyyy}</p>
            <p>Este comprobante es un documento válido que certifica el pago de nómina según las disposiciones legales colombianas.</p>
            <p>Conserve este documento para sus registros personales.</p>
        </div>

        <div class='signature-area'>
            <div class='signature-line'>
                <p>Firma Empleado</p>
            </div>
            <div class='signature-line'>
                <p>Autorizado por</p>
            </div>
        </div>
    </div>
</body>
</html>";

                File.WriteAllText(path, html, Encoding.UTF8);
                
                // Log export operation
                AuditLogger.LogExport("Nomina", $"PDF payroll slip for {data.EmployeeName}, period {data.PayPeriodStart:yyyy-MM-dd}");
                
                // Open file
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                return true;
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowValidationError($"Error generando comprobante de nómina: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Load payroll slip data from PayrollLog and Employee records
        /// </summary>
        public static PayslipData LoadPayslipData(int payrollLogId)
        {
            try
            {
                payrollModel payrollMdl = new payrollModel();
                empleadosModel empMdl = new empleadosModel();

                // Query payroll log
                const string payrollQuery = @"
                    SELECT 
                        pl.Id, pl.EmployeeId, pl.PayPeriodStart, pl.PayPeriodEnd, 
                        pl.GrossPay, pl.Deductions, pl.NetPay, pl.PaymentDate
                    FROM Payroll_Log pl
                    WHERE pl.Id = @id";

                var parameters = new Dictionary<string, object> { ["@id"] = payrollLogId };
                DataTable payrollData = new conexionModel().ejecutarConsultaParametrizada(payrollQuery, parameters);

                if (payrollData == null || payrollData.Rows.Count == 0)
                    return null;

                DataRow payrow = payrollData.Rows[0];
                int empId = Convert.ToInt32(payrow["EmployeeId"]);
                decimal grossPay = Convert.ToDecimal(payrow["GrossPay"]);

                // Query employee info
                const string empQuery = @"
                    SELECT 
                        Id, Nombre, Apellido, Puesto, Cedula
                    FROM Empleados
                    WHERE Id = @id";

                var empParams = new Dictionary<string, object> { ["@id"] = empId };
                DataTable empData = new conexionModel().ejecutarConsultaParametrizada(empQuery, empParams);

                if (empData == null || empData.Rows.Count == 0)
                    return null;

                DataRow emprow = empData.Rows[0];

                // Calculate deductions using PayrollBreakdown
                var breakdown = PayrollBreakdown.Calculate(grossPay);

                return new PayslipData
                {
                    PayrollLogId = payrollLogId,
                    EmployeeName = $"{emprow["Nombre"]} {emprow["Apellido"]}",
                    EmployeePosition = emprow["Puesto"].ToString(),
                    EmployeeDocument = emprow["Cedula"].ToString(),
                    PayPeriodStart = Convert.ToDateTime(payrow["PayPeriodStart"]),
                    PayPeriodEnd = Convert.ToDateTime(payrow["PayPeriodEnd"]),
                    GrossPay = grossPay,
                    SaludDeduction = breakdown.SaludEmpleado,
                    PensionDeduction = breakdown.PensionEmpleado,
                    FondoSolidaridad = breakdown.FondoSolidaridad,
                    Parafiscales = breakdown.Parafiscales,
                    Cesantias = breakdown.Cesantias,
                    PrimaServicios = breakdown.PrimaServicios,
                    Vacaciones = breakdown.Vacaciones,
                    TotalDeductions = breakdown.TotalDeductions,
                    NetPay = grossPay - breakdown.TotalDeductions,
                    PaymentDate = Convert.ToDateTime(payrow["PaymentDate"])
                };
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowValidationError($"Error loading payroll data: {ex.Message}");
                return null;
            }
        }
    }
}

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
    /// FASE 3 - Tasks 33: Sales Report Excel Export
    /// Generates period sales reports filtered by date range
    /// Exports to CSV format (Excel-compatible)
    /// </summary>
    public static class SalesReportExporter
    {
        /// <summary>
        /// Task 33: Generate sales report for period
        /// Columns: Cliente, Fecha, Subtotal, IVA, Total
        /// </summary>
        public static bool GenerateSalesReportExcel(DateTime startDate, DateTime endDate)
        {
            try
            {
                conexionModel conexion = new conexionModel();

                // Query invoices for period with client name
                const string query = @"
                    SELECT 
                        c.Nombre AS 'Cliente',
                        CONVERT(VARCHAR(10), i.IssueDate, 103) AS 'Fecha',
                        i.Subtotal,
                        i.TotalTax AS 'IVA',
                        i.Total,
                        i.PaymentStatus AS 'Estado'
                    FROM Invoices i
                    JOIN Clients c ON i.ClientId = c.Id
                    WHERE i.IssueDate >= @startDate 
                      AND i.IssueDate <= @endDate
                    ORDER BY i.IssueDate DESC";

                var parameters = new Dictionary<string, object>
                {
                    ["@startDate"] = startDate,
                    ["@endDate"] = endDate
                };

                DataTable salesData = conexion.ejecutarConsultaParametrizada(query, parameters);

                if (salesData == null || salesData.Rows.Count == 0)
                {
                    ValidationHelper.ShowValidationError("No hay facturas para el período seleccionado.");
                    return false;
                }

                string filename = $"Reporte_Ventas_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}";
                return ExportToExcelCSV(salesData, filename, $"Reporte de Ventas {startDate:yyyy-MM-dd} a {endDate:yyyy-MM-dd}");
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowValidationError($"Error generando reporte de ventas: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Export DataTable to CSV format (Excel-compatible)
        /// Includes report title and date range
        /// </summary>
        private static bool ExportToExcelCSV(DataTable data, string filename, string reportTitle)
        {
            try
            {
                var coCulture = new System.Globalization.CultureInfo("es-CO");
                string path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"{filename}_{DateTime.Now:HHmmss}.csv");

                StringBuilder sb = new StringBuilder();

                // Add report title
                sb.AppendLine(reportTitle);
                sb.AppendLine($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                sb.AppendLine();

                // Write Headers
                string[] columnNames = new string[data.Columns.Count];
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    columnNames[i] = $"\"{data.Columns[i].ColumnName}\"";
                }
                sb.AppendLine(string.Join(",", columnNames));

                // Write Rows
                decimal totalSubtotal = 0, totalIVA = 0, totalAmount = 0;
                foreach (DataRow row in data.Rows)
                {
                    string[] fields = new string[data.Columns.Count];
                    for (int i = 0; i < data.Columns.Count; i++)
                    {
                        object value = row[i];

                        // Format currency columns
                        if ((data.Columns[i].ColumnName == "Subtotal" ||
                             data.Columns[i].ColumnName == "IVA" ||
                             data.Columns[i].ColumnName == "Total") &&
                            value is decimal decValue)
                        {
                            fields[i] = decValue.ToString("N2", coCulture);
                            // Accumulate totals
                            if (data.Columns[i].ColumnName == "Subtotal")
                                totalSubtotal += decValue;
                            else if (data.Columns[i].ColumnName == "IVA")
                                totalIVA += decValue;
                            else if (data.Columns[i].ColumnName == "Total")
                                totalAmount += decValue;
                        }
                        else
                        {
                            fields[i] = $"\"{(value?.ToString() ?? "").Replace("\"", "\"\"")}\"";
                        }
                    }
                    sb.AppendLine(string.Join(",", fields));
                }

                // Add totals row
                sb.AppendLine();
                sb.AppendLine($"\"TOTALES\",\"\",\"{totalSubtotal:N2}\",\"{totalIVA:N2}\",\"{totalAmount:N2}\",\"\"");

                File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

                // Log export
                AuditLogger.LogExport("Reportes_Ventas", $"Sales report export: {data.Rows.Count} invoices");

                // Open file
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                return true;
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowValidationError($"Error exporting to Excel: {ex.Message}");
                return false;
            }
        }
    }

    /// <summary>
    /// FASE 3 - Task 34: Payroll Report Excel Export
    /// Generates payroll reports for all employees in a period
    /// Columns: Nombre, Cargo, Bruto, Deducciones, Neto
    /// </summary>
    public static class PayrollReportExporter
    {
        /// <summary>
        /// Task 34: Generate payroll report for period
        /// Shows all processed employees with breakdown
        /// </summary>
        public static bool GeneratePayrollReportExcel(DateTime startDate, DateTime endDate)
        {
            try
            {
                conexionModel conexion = new conexionModel();

                // Query all payroll entries for period grouped by employee
                const string query = @"
                    SELECT 
                        e.Nombre + ' ' + e.Apellido AS 'Empleado',
                        e.Puesto AS 'Cargo',
                        pl.GrossPay AS 'Bruto',
                        pl.Deductions AS 'Deducciones',
                        pl.NetPay AS 'Neto',
                        CONVERT(VARCHAR(10), pl.PayPeriodStart, 103) AS 'Período Inicio',
                        CONVERT(VARCHAR(10), pl.PayPeriodEnd, 103) AS 'Período Fin'
                    FROM Payroll_Log pl
                    JOIN Empleados e ON pl.EmployeeId = e.Id
                    WHERE pl.PayPeriodStart >= @startDate 
                      AND pl.PayPeriodEnd <= @endDate
                    ORDER BY e.Nombre, pl.PayPeriodEnd DESC";

                var parameters = new Dictionary<string, object>
                {
                    ["@startDate"] = startDate,
                    ["@endDate"] = endDate
                };

                DataTable payrollData = conexion.ejecutarConsultaParametrizada(query, parameters);

                if (payrollData == null || payrollData.Rows.Count == 0)
                {
                    ValidationHelper.ShowValidationError("No hay registros de nómina para el período seleccionado.");
                    return false;
                }

                string filename = $"Reporte_Nomina_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}";
                return ExportPayrollToExcelCSV(payrollData, filename, $"Reporte de Nómina {startDate:yyyy-MM-dd} a {endDate:yyyy-MM-dd}");
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowValidationError($"Error generando reporte de nómina: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Export payroll DataTable to CSV format with totals
        /// </summary>
        private static bool ExportPayrollToExcelCSV(DataTable data, string filename, string reportTitle)
        {
            try
            {
                var coCulture = new System.Globalization.CultureInfo("es-CO");
                string path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"{filename}_{DateTime.Now:HHmmss}.csv");

                StringBuilder sb = new StringBuilder();

                // Add report title
                sb.AppendLine(reportTitle);
                sb.AppendLine($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                sb.AppendLine();

                // Write Headers
                string[] columnNames = new string[data.Columns.Count];
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    columnNames[i] = $"\"{data.Columns[i].ColumnName}\"";
                }
                sb.AppendLine(string.Join(",", columnNames));

                // Write Rows
                decimal totalBruto = 0, totalDeducciones = 0, totalNeto = 0;
                foreach (DataRow row in data.Rows)
                {
                    string[] fields = new string[data.Columns.Count];
                    for (int i = 0; i < data.Columns.Count; i++)
                    {
                        object value = row[i];

                        // Format currency columns
                        if ((data.Columns[i].ColumnName == "Bruto" ||
                             data.Columns[i].ColumnName == "Deducciones" ||
                             data.Columns[i].ColumnName == "Neto") &&
                            value is decimal decValue)
                        {
                            fields[i] = decValue.ToString("N2", coCulture);
                            // Accumulate totals
                            if (data.Columns[i].ColumnName == "Bruto")
                                totalBruto += decValue;
                            else if (data.Columns[i].ColumnName == "Deducciones")
                                totalDeducciones += decValue;
                            else if (data.Columns[i].ColumnName == "Neto")
                                totalNeto += decValue;
                        }
                        else
                        {
                            fields[i] = $"\"{(value?.ToString() ?? "").Replace("\"", "\"\"")}\"";
                        }
                    }
                    sb.AppendLine(string.Join(",", fields));
                }

                // Add totals row
                sb.AppendLine();
                sb.AppendLine($"\"TOTALES\",\"\",\"{totalBruto:N2}\",\"{totalDeducciones:N2}\",\"{totalNeto:N2}\",\"\",\"\"");

                File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

                // Log export
                AuditLogger.LogExport("Reportes_Nomina", $"Payroll report export: {data.Rows.Count} records");

                // Open file
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                return true;
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowValidationError($"Error exporting to Excel: {ex.Message}");
                return false;
            }
        }
    }
}

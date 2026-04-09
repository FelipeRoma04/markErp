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
    /// FASE 3 - Task 35: Critical Stock Report (PDF)
    /// Exports inventory status with critical stock alerts
    /// Shows current stock, minimum stock, and status indicator
    /// </summary>
    public static class CriticalStockReportPDF
    {
        public enum StockStatus
        {
            CRITICAL,      // Stock < Min
            WARNING,       // Stock between Min and 150% of Min
            OK             // Stock > 150% of Min
        }

        /// <summary>
        /// Task 35: Generate critical stock report PDF
        /// Lists all products with stock status
        /// </summary>
        public static bool GenerateCriticalStockReportPDF()
        {
            try
            {
                conexionModel conexion = new conexionModel();

                // Query all products with stock status
                const string query = @"
                    SELECT 
                        p.Id,
                        p.Name,
                        p.Category,
                        p.Stock AS 'StockActual',
                        p.MinStock AS 'StockMinimo',
                        p.Price,
                        CASE 
                            WHEN p.Stock <= p.MinStock THEN 'CRÍTICO'
                            WHEN p.Stock <= (p.MinStock * 1.5) THEN 'ALERTA'
                            ELSE 'OK'
                        END AS 'Estado'
                    FROM Products p
                    ORDER BY p.Category, p.Stock ASC";

                DataTable stockData = conexion.ejecutarConsulta(query);

                if (stockData == null || stockData.Rows.Count == 0)
                {
                    ValidationHelper.ShowValidationError("No hay productos en inventario.");
                    return false;
                }

                return ExportStockReportPDF(stockData);
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowValidationError($"Error generando reporte: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Generate HTML report with stock status
        /// </summary>
        private static bool ExportStockReportPDF(DataTable stockData)
        {
            try
            {
                var coCulture = new System.Globalization.CultureInfo("es-CO");
                string path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"Reporte_Inventario_Critico_{DateTime.Now:yyyyMMdd_HHmmss}.html");

                // Calculate statistics
                int criticalCount = 0, warningCount = 0, okCount = 0;
                decimal totalValue = 0;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(@"<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Reporte de Inventario Crítico</title>
    <style>
        body { font-family: Arial, sans-serif; padding: 20px; background-color: #f9f9f9; }
        .container { max-width: 1200px; margin: 0 auto; background-color: #fff; padding: 30px; box-shadow: 0 0 10px rgba(0,0,0,0.1); }
        .header { text-align: center; border-bottom: 3px solid #d9534f; padding-bottom: 20px; margin-bottom: 30px; }
        .header h1 { margin: 0; color: #333; }
        .header p { margin: 5px 0; color: #666; }
        .alert-box { display: flex; justify-content: space-around; margin-bottom: 30px; }
        .alert-item { flex: 1; margin: 0 10px; padding: 15px; border-radius: 5px; text-align: center; }
        .critical { background-color: #f8d7da; border: 2px solid #f5c6cb; color: #721c24; }
        .warning { background-color: #fff3cd; border: 2px solid #ffeaa7; color: #856404; }
        .ok { background-color: #d4edda; border: 2px solid #c3e6cb; color: #155724; }
        .alert-item h3 { margin: 0 0 10px 0; }
        .alert-item .count { font-size: 28px; font-weight: bold; }
        table { width: 100%; border-collapse: collapse; margin: 20px 0; }
        th { background-color: #4a4a4a; color: #fff; padding: 12px; text-align: left; font-weight: bold; }
        td { padding: 10px; border-bottom: 1px solid #ddd; }
        tr:nth-child(even) { background-color: #f5f5f5; }
        .category { background-color: #e9ecef; font-weight: bold; padding: 12px; }
        .critical-row { background-color: #f8d7da; }
        .warning-row { background-color: #fff3cd; }
        .ok-row { background-color: #d4edda; }
        .status { font-weight: bold; }
        .text-right { text-align: right; }
        .footer { text-align: center; margin-top: 40px; padding-top: 20px; border-top: 1px solid #ddd; color: #666; font-size: 12px; }
        .semaforo { display: inline-block; width: 20px; height: 20px; border-radius: 50%; margin-right: 5px; vertical-align: middle; }
        .rojo { background-color: #dc3545; }
        .amarillo { background-color: #ffc107; }
        .verde { background-color: #28a745; }
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>REPORTE DE INVENTARIO CRÍTICO</h1>
            <p>Generado: " + DateTime.Now.ToString("dd/MMMM/yyyy HH:mm:ss") + @"</p>
        </div>

        <div class='alert-box'>");

                // Count statuses
                foreach (DataRow row in stockData.Rows)
                {
                    string estado = row["Estado"].ToString();
                    if (estado == "CRÍTICO") criticalCount++;
                    else if (estado == "ALERTA") warningCount++;
                    else okCount++;
                }

                sb.AppendLine($@"
            <div class='alert-item critical'>
                <h3>🔴 CRÍTICO</h3>
                <div class='count'>{criticalCount}</div>
                <p>Por debajo del mínimo</p>
            </div>
            <div class='alert-item warning'>
                <h3>🟡 ALERTA</h3>
                <div class='count'>{warningCount}</div>
                <p>Entre mínimo y 150%</p>
            </div>
            <div class='alert-item ok'>
                <h3>🟢 OK</h3>
                <div class='count'>{okCount}</div>
                <p>Nivel normal</p>
            </div>
        </div>

        <table>
            <tr>
                <th>Categoría</th>
                <th>Producto</th>
                <th class='text-right'>Stock Actual</th>
                <th class='text-right'>Stock Mínimo</th>
                <th class='text-right'>Precio Unit.</th>
                <th>Estado</th>
            </tr>");

                string currentCategory = "";
                foreach (DataRow row in stockData.Rows)
                {
                    string category = row["Category"].ToString();
                    if (category != currentCategory)
                    {
                        sb.AppendLine($"<tr class='category'><td colspan='6'>{category}</td></tr>");
                        currentCategory = category;
                    }

                    string estado = row["Estado"].ToString();
                    string rowClass = estado == "CRÍTICO" ? "critical-row" : (estado == "ALERTA" ? "warning-row" : "ok-row");
                    string icon = estado == "CRÍTICO" ? "🔴" : (estado == "ALERTA" ? "🟡" : "🟢");

                    decimal stock = Convert.ToDecimal(row["StockActual"]);
                    decimal minStock = Convert.ToDecimal(row["StockMinimo"]);
                    decimal price = Convert.ToDecimal(row["Price"]);
                    decimal lineValue = stock * price;
                    totalValue += lineValue;

                    sb.AppendLine($@"
            <tr class='{rowClass}'>
                <td>{category}</td>
                <td>{row["Name"]}</td>
                <td class='text-right'>{stock:N0}</td>
                <td class='text-right'>{minStock:N0}</td>
                <td class='text-right'>{price.ToString("C", coCulture)}</td>
                <td><span class='status'>{icon} {estado}</span></td>
            </tr>");
                }

                sb.AppendLine($@"
            <tr style='background-color: #e9ecef; font-weight: bold;'>
                <td colspan='4'>TOTAL VALOR INVENTARIO</td>
                <td class='text-right'>{totalValue.ToString("C", coCulture)}</td>
                <td></td>
            </tr>
        </table>

        <div class='footer'>
            <p><strong>Leyenda:</strong></p>
            <p><span class='semaforo rojo'></span> Crítico: Stock ≤ Mínimo</p>
            <p><span class='semaforo amarillo'></span> Alerta: Mínimo < Stock ≤ 150% Mínimo</p>
            <p><span class='semaforo verde'></span> Correcto: Stock > 150% Mínimo</p>
            <p>Se recomienda realizar compras para productos en estado CRÍTICO.</p>
        </div>
    </div>
</body>
</html>");

                File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

                // Log export
                AuditLogger.LogExport("Reportes_Inventario", $"Critical stock report: {criticalCount} critical, {warningCount} warning");

                // Open file
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                return true;
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowValidationError($"Error exporting report: {ex.Message}");
                return false;
            }
        }
    }
}

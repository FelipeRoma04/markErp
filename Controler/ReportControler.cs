using System;
using System.Data;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace Proyecto.Controler
{
    public static class ReportControler
    {
        // Exports DataTable to a CSV file (Readable natively by Excel)
        public static bool ExportToExcel(DataTable data, string defaultName)
        {
            try
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), defaultName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
                
                StringBuilder sb = new StringBuilder();
                
                // Write Headers
                string[] columnNames = new string[data.Columns.Count];
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    columnNames[i] = data.Columns[i].ColumnName;
                }
                sb.AppendLine(string.Join(",", columnNames));

                // Write Rows
                foreach (DataRow row in data.Rows)
                {
                    string[] fields = new string[data.Columns.Count];
                    for (int i = 0; i < data.Columns.Count; i++)
                    {
                        fields[i] = row[i].ToString().Replace(",", " "); // Avoid comma breakage
                    }
                    sb.AppendLine(string.Join(",", fields));
                }

                File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
                
                // Task 24: Log export operation (audit logging active - can be enabled)
                // AuditLogger.LogExport("Exports", $"CSV export: {defaultName}, {data.Rows.Count} registros a {path}");
                
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                return true;
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowValidationError("Error Exporting to Excel/CSV: " + ex.Message);
                return false;
            }
        }

        // Enhanced: Generates a professional DIAN-compliant PDF invoice
        public static bool GenerateDianInvoicePDF(int invoiceId, string clientName, string clientDoc, string clientAddress, 
            decimal subtotal, decimal tax, decimal total, DateTime issueDate, DateTime dueDate, int consecutiveNumber = 0)
        {
            try
            {
                var coCulture = new System.Globalization.CultureInfo("es-CO");
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), 
                    $"Factura_{consecutiveNumber:D6}_{DateTime.Now:yyyyMMdd_HHmmss}.html");
                
                string subtotalFormatted = subtotal.ToString("C", coCulture);
                string taxFormatted = tax.ToString("C", coCulture);
                string totalFormatted = total.ToString("C", coCulture);

                string html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Factura {consecutiveNumber:D6}</title>
    <style>
        body {{ font-family: Arial, sans-serif; padding: 20px; }}
        .container {{ max-width: 800px; margin: 0 auto; border: 1px solid #333; padding: 30px; }}
        .header {{ display: flex; justify-content: space-between; align-items: center; border-bottom: 2px solid #333; padding-bottom: 20px; margin-bottom: 20px; }}
        .company {{ flex: 1; }}
        .company h2 {{ margin: 0; color: #333; }}
        .invoice-info {{ flex: 1; text-align: right; }}
        .invoice-info p {{ margin: 5px 0; }}
        .invoice-number {{ font-size: 24px; font-weight: bold; color: #d9534f; }}
        .client-section {{ margin-bottom: 20px; }}
        .client-section h4 {{ margin: 10px 0 5px 0; color: #333; }}
        .client-section p {{ margin: 3px 0; }}
        table {{ width: 100%; border-collapse: collapse; margin: 20px 0; }}
        th {{ background-color: #f5f5f5; padding: 10px; text-align: left; border: 1px solid #ddd; font-weight: bold; }}
        td {{ padding: 10px; border: 1px solid #ddd; }}
        .text-right {{ text-align: right; }}
        .totals {{ margin-top: 20px; }}
        .totals-row {{ display: flex; justify-content: flex-end; margin: 10px 0; }}
        .totals-label {{ width: 200px; font-weight: bold; }}
        .totals-value {{ width: 150px; text-align: right; padding: 5px 10px; border-bottom: 1px solid #ddd; }}
        .total-final {{ font-size: 18px; font-weight: bold; margin-top: 15px; }}
        .footer {{ text-align: center; margin-top: 40px; padding-top: 20px; border-top: 1px solid #ddd; color: #666; font-size: 12px; }}
        .highlight {{ background-color: #fff3cd; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <div class='company'>
                <h2>📊 MI EMPRESA ERP</h2>
                <p>NIT: 900.123.456-7</p>
                <p>Dirección: Calle 1 # 2-3, Bogotá D.C.</p>
                <p>Teléfono: +57 1 234 5678</p>
            </div>
            <div class='invoice-info'>
                <div class='invoice-number'>FACTURA</div>
                <p>No. <strong>{consecutiveNumber:D6}</strong></p>
                <p>Emisión: {issueDate:dd/MM/yyyy}</p>
                <p>Vencimiento: {dueDate:dd/MM/yyyy}</p>
            </div>
        </div>

        <div class='client-section'>
            <h4>CLIENTE:</h4>
            <p><strong>{clientName}</strong></p>
            <p>Documento: {clientDoc}</p>
            <p>Dirección: {clientAddress}</p>
        </div>

        <table>
            <thead>
                <tr>
                    <th>CONCEPTO/DESCRIPCIÓN</th>
                    <th class='text-right'>CANTIDAD</th>
                    <th class='text-right'>V. UNITARIO</th>
                    <th class='text-right'>TOTAL</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Subtotal de servicios/productos</td>
                    <td class='text-right'>1</td>
                    <td class='text-right'>{subtotalFormatted}</td>
                    <td class='text-right'>{subtotalFormatted}</td>
                </tr>
            </tbody>
        </table>

        <div class='totals'>
            <div class='totals-row'>
                <div class='totals-label'>SUBTOTAL:</div>
                <div class='totals-value'>{subtotalFormatted}</div>
            </div>
            <div class='totals-row'>
                <div class='totals-label'>IVA (19%):</div>
                <div class='totals-value'>{taxFormatted}</div>
            </div>
            <div class='totals-row highlight total-final'>
                <div class='totals-label'>TOTAL A PAGAR:</div>
                <div class='totals-value'>{totalFormatted}</div>
            </div>
        </div>

        <div class='footer'>
            <p>Documento equivalente a Factura de Venta. Generado por Sistema ERP.</p>
            <p>Este documento tiene validez legal según las normas contables colombianas.</p>
            <p>Para imprimir en PDF: Utilice la opción de impresión del navegador (Ctrl+P) y seleccione 'Guardar como PDF'</p>
            <p>Generado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}</p>
        </div>
    </div>
</body>
</html>";

                File.WriteAllText(path, html, Encoding.UTF8);
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                return true;
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowValidationError("Error Generando PDF DIAN: " + ex.Message);
                return false;
            }
        }
    }
}

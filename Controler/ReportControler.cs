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
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                return true;
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowValidationError("Error Exporting to Excel/CSV: " + ex.Message);
                return false;
            }
        }

        // Generates an HTML invoice for native 'Print to PDF' capabilities
        public static bool ExportInvoiceNativePDF(int invoiceId, string clientName, decimal subtotal, decimal tax, decimal total)
        {
            try
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Factura_" + invoiceId + ".html");
                
                string html = $@"
                <html>
                <body style='font-family: Arial; padding: 40px;'>
                    <div style='border: 1px solid #ccc; padding: 20px; text-align: center'>
                        <h1>Mi Empresa ERP</h1>
                        <h3>DOCUMENTO EQUIVALENTE A FACTURA</h3>
                        <hr/>
                        <p>Factura No. <b>{invoiceId}</b></p>
                        <p>Cliente: <b>{clientName}</b></p>
                        <p>Fecha de Emisión: <b>{DateTime.Now.ToShortDateString()}</b></p>
                        <br/>
                        <table style='width:100%; text-align:left;' border='1' cellspacing='0' cellpadding='5'>
                            <tr><th>Concepto</th><th>Monto</th></tr>
                            <tr><td>Subtotal</td><td>{subtotal:C}</td></tr>
                            <tr><td>IVA (19%)</td><td>{tax:C}</td></tr>
                            <tr style='background-color:#eee'><td><b>Total a Pagar</b></td><td><b>{total:C}</b></td></tr>
                        </table>
                        <br/><p><i>Puede imprimir este documento guardándolo como PDF pulsando Ctrl+P.</i></p>
                    </div>
                </body>
                </html>";

                File.WriteAllText(path, html, Encoding.UTF8);
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                return true;
            }
            catch(Exception ex)
            {
                ValidationHelper.ShowValidationError("Error Exporting PDF/HTML: " + ex.Message);
                return false;
            }
        }
    }
}

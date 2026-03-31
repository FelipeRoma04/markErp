using System;
using System.IO;
using System.Text;

namespace Proyecto.Controler
{
    /// <summary>
    /// PDF export helper (simplified - iTextSharp not available)
    /// Provides basic text file export as placeholder
    /// </summary>
    public static class PdfHelper
    {
        private const string CompanyName = "markERP";
        private const string CompanyAddress = "Calle 123, Ciudad, Colombia";
        private const string CompanyPhone = "+57 1 2345678";

        /// <summary>
        /// Export invoice to text file (PDF export requires iTextSharp installation)
        /// </summary>
        public static bool ExportInvoiceToPdf(int invoiceId, string filePath, string clientName, 
            decimal subtotal, decimal tax, decimal total, DateTime date)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("════════════════════════════════════════════════════════════════");
                sb.AppendLine("                           " + CompanyName);
                sb.AppendLine(CompanyAddress + " | Tel: " + CompanyPhone);
                sb.AppendLine("════════════════════════════════════════════════════════════════");
                sb.AppendLine();
                sb.AppendLine("                         FACTURA # " + invoiceId);
                sb.AppendLine();
                sb.AppendLine("Fecha: " + date.ToString("dd/MM/yyyy"));
                sb.AppendLine("Cliente: " + clientName);
                sb.AppendLine();
                sb.AppendLine("────────────────────────────────────────────────────────────────");
                sb.AppendLine("CONCEPTO".PadRight(40) + "VALOR".PadLeft(20));
                sb.AppendLine("────────────────────────────────────────────────────────────────");
                sb.AppendLine("Subtotal".PadRight(40) + subtotal.ToString("C").PadLeft(20));
                sb.AppendLine("IVA (19%)".PadRight(40) + tax.ToString("C").PadLeft(20));
                sb.AppendLine("────────────────────────────────────────────────────────────────");
                sb.AppendLine("TOTAL".PadRight(40) + total.ToString("C").PadLeft(20));
                sb.AppendLine("────────────────────────────────────────────────────────────────");
                sb.AppendLine();
                sb.AppendLine("Gracias por su compra.");
                sb.AppendLine();
                sb.AppendLine("════════════════════════════════════════════════════════════════");

                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Export payroll to text file (PDF export requires iTextSharp installation)
        /// </summary>
        public static bool ExportPayrollToPdf(int employeeId, string employeeName, string filePath,
            decimal gross, decimal deductions, decimal net, DateTime periodStart, DateTime periodEnd)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("════════════════════════════════════════════════════════════════");
                sb.AppendLine("                           " + CompanyName);
                sb.AppendLine(CompanyAddress + " | Tel: " + CompanyPhone);
                sb.AppendLine("════════════════════════════════════════════════════════════════");
                sb.AppendLine();
                sb.AppendLine("                      DESPRENDIBLE DE NÓMINA");
                sb.AppendLine();
                sb.AppendLine("Periodo: " + periodStart.ToString("dd/MM/yyyy") + " al " + periodEnd.ToString("dd/MM/yyyy"));
                sb.AppendLine("Empleado: " + employeeName);
                sb.AppendLine();
                sb.AppendLine("────────────────────────────────────────────────────────────────");
                sb.AppendLine("CONCEPTO".PadRight(40) + "VALOR".PadLeft(20));
                sb.AppendLine("────────────────────────────────────────────────────────────────");
                sb.AppendLine("Salario Bruto".PadRight(40) + gross.ToString("C").PadLeft(20));
                sb.AppendLine("Descuentos".PadRight(40) + ("-" + deductions.ToString("C")).PadLeft(20));
                sb.AppendLine("────────────────────────────────────────────────────────────────");
                sb.AppendLine("SALARIO NETO".PadRight(40) + net.ToString("C").PadLeft(20));
                sb.AppendLine("────────────────────────────────────────────────────────────────");
                sb.AppendLine();
                sb.AppendLine("Descuentos legales: Salud 4%, Pensión 4%");
                sb.AppendLine();
                sb.AppendLine("════════════════════════════════════════════════════════════════");

                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

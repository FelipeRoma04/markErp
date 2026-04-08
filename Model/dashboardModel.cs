using System;
using System.Data;
using System.IO;

namespace Proyecto.Model
{
    internal class dashboardModel
    {
        conexionModel conexion;

        public dashboardModel()
        {
            conexion = new conexionModel();
        }

        public int GetTotalProducts()
        {
            try
            {
                DataTable dt = conexion.ejecutarConsulta("SELECT COUNT(*) as Total FROM Products");
                if (dt != null && dt.Rows.Count > 0) return Convert.ToInt32(dt.Rows[0]["Total"]);
                return 0;
            }
            catch (Exception ex)
            {
                try
                {
                    var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cliente-crash.log");
                    File.AppendAllText(logPath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] dashboard.GetTotalProducts error: {ex}\n\n");
                }
                catch { }
                return 0;
            }
        }

        public decimal GetTotalSales()
        {
            try
            {
                DataTable dt = conexion.ejecutarConsulta("SELECT SUM(Total) as Total FROM Invoices");
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["Total"] != DBNull.Value) return Convert.ToDecimal(dt.Rows[0]["Total"]);
                return 0;
            }
            catch (Exception ex)
            {
                try
                {
                    var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cliente-crash.log");
                    File.AppendAllText(logPath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] dashboard.GetTotalSales error: {ex}\n\n");
                }
                catch { }
                return 0;
            }
        }

        public KpiSnapshot GetKpis()
        {
            KpiSnapshot result = new KpiSnapshot();
            try
            {
                result.TotalProducts = GetTotalProducts();
                result.TotalSales = GetTotalSales();

                var pendingDt = conexion.ejecutarConsulta(@"
                    WITH pay AS (SELECT InvoiceId, SUM(Amount) AS Paid FROM Payments GROUP BY InvoiceId)
                    SELECT COUNT(*) AS PendingInvoices,
                           SUM(CASE WHEN i.DueDate < GETDATE() AND (i.Total-ISNULL(p.Paid,0))>0 THEN 1 ELSE 0 END) AS OverdueCount,
                           SUM(CASE WHEN i.DueDate < GETDATE() THEN i.Total-ISNULL(p.Paid,0) ELSE 0 END) AS OverdueAmount
                    FROM Invoices i
                    LEFT JOIN pay p ON p.InvoiceId = i.Id
                    WHERE i.Total-ISNULL(p.Paid,0) > 0");

                if (pendingDt != null && pendingDt.Rows.Count > 0)
                {
                    result.PendingInvoices = Convert.ToInt32(pendingDt.Rows[0]["PendingInvoices"]);
                    result.OverdueCount = Convert.ToInt32(pendingDt.Rows[0]["OverdueCount"]);
                    result.OverdueAmount = pendingDt.Rows[0]["OverdueAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(pendingDt.Rows[0]["OverdueAmount"]);
                }

                var clients = conexion.ejecutarConsulta("SELECT COUNT(*) AS Total FROM Clients");
                if (clients != null && clients.Rows.Count > 0)
                    result.TotalClients = Convert.ToInt32(clients.Rows[0]["Total"]);
            }
            catch (Exception ex)
            {
                try
                {
                    var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cliente-crash.log");
                    File.AppendAllText(logPath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] dashboard.GetKpis error: {ex}\n\n");
                }
                catch { }
            }
            return result;
        }
    }

    internal class KpiSnapshot
    {
        public int TotalProducts { get; set; }
        public decimal TotalSales { get; set; }
        public int PendingInvoices { get; set; }
        public int OverdueCount { get; set; }
        public decimal OverdueAmount { get; set; }
        public int TotalClients { get; set; }
    }
}

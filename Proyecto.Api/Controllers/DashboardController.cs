using Microsoft.AspNetCore.Mvc;
using Proyecto.Api.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly DbService _db;
        public DashboardController(DbService db) => _db = db;

        // GET /api/dashboard/kpis
        [HttpGet("kpis")]
        public async Task<IActionResult> GetKpis()
        {
            var products = await _db.QueryAsync("SELECT COUNT(*) AS Total FROM Products");
            var invoices = await _db.QueryAsync("SELECT SUM(Total) AS Total FROM Invoices");

            var payAgg = await _db.QueryAsync(@"
                WITH pay AS (SELECT InvoiceId, SUM(Amount) AS Paid FROM Payments GROUP BY InvoiceId)
                SELECT COUNT(*) AS PendingInvoices,
                       SUM(CASE WHEN i.DueDate < GETDATE() AND (i.Total-ISNULL(p.Paid,0))>0 THEN 1 ELSE 0 END) AS OverdueCount,
                       SUM(CASE WHEN i.DueDate < GETDATE() THEN i.Total-ISNULL(p.Paid,0) ELSE 0 END) AS OverdueAmount
                FROM Invoices i
                LEFT JOIN pay p ON p.InvoiceId = i.Id
                WHERE i.Total-ISNULL(p.Paid,0) > 0");

            var clients = await _db.QueryAsync("SELECT COUNT(*) AS Total FROM Clients");

            int totalProducts = products.Rows.Count > 0 && products.Rows[0]["Total"] != DBNull.Value ? Convert.ToInt32(products.Rows[0]["Total"]) : 0;
            decimal totalSales = invoices.Rows.Count > 0 && invoices.Rows[0]["Total"] != DBNull.Value ? Convert.ToDecimal(invoices.Rows[0]["Total"]) : 0;
            int pendingInvoices = payAgg.Rows.Count > 0 && payAgg.Rows[0]["PendingInvoices"] != DBNull.Value ? Convert.ToInt32(payAgg.Rows[0]["PendingInvoices"]) : 0;
            int overdueCount = payAgg.Rows.Count > 0 && payAgg.Rows[0]["OverdueCount"] != DBNull.Value ? Convert.ToInt32(payAgg.Rows[0]["OverdueCount"]) : 0;
            decimal overdueAmount = payAgg.Rows.Count > 0 && payAgg.Rows[0]["OverdueAmount"] != DBNull.Value ? Convert.ToDecimal(payAgg.Rows[0]["OverdueAmount"]) : 0;
            int totalClients = clients.Rows.Count > 0 && clients.Rows[0]["Total"] != DBNull.Value ? Convert.ToInt32(clients.Rows[0]["Total"]) : 0;

            return Ok(new
            {
                totalProducts,
                totalSales,
                pendingInvoices,
                overdueCount,
                overdueAmount,
                totalClients
            });
        }
    }
}

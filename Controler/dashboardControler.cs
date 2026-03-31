using System;
using Proyecto.Model;

namespace Proyecto.Controler
{
    internal class dashboardControler
    {
        public string GetKpiSummary()
        {
            dashboardModel model = new dashboardModel();
            int totalProducts = model.GetTotalProducts();
            decimal totalSales = model.GetTotalSales();

            return $"KPI OVERVIEW: \n\n Productos en Inventario: {totalProducts}\n Facturación Histórica: {totalSales:C}";
        }
    }
}

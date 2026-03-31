using System;
using System.Data;

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
            DataTable dt = conexion.ejecutarConsulta("SELECT COUNT(*) as Total FROM Products");
            if(dt != null && dt.Rows.Count > 0) return Convert.ToInt32(dt.Rows[0]["Total"]);
            return 0;
        }

        public decimal GetTotalSales()
        {
            DataTable dt = conexion.ejecutarConsulta("SELECT SUM(Total) as Total FROM Invoices");
            if(dt != null && dt.Rows.Count > 0 && dt.Rows[0]["Total"] != DBNull.Value) return Convert.ToDecimal(dt.Rows[0]["Total"]);
            return 0;
        }
    }
}

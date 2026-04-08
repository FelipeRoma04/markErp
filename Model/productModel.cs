using System;
using System.Data;

namespace Proyecto.Model
{
    internal class productModel
    {
        conexionModel conexion;

        public productModel()
        {
            conexion = new conexionModel();
        }

        public bool InsertProduct(string code, string name, decimal cost, decimal sale, int stock, int minStock)
        {
            var sql = "INSERT INTO Products (Code, Name, CostPrice, SalePrice, Stock, MinStock) VALUES (@code, @name, @cost, @sale, @stock, @min)";
            var p = new System.Collections.Generic.Dictionary<string, object>
            {
                ["@code"] = code,
                ["@name"] = name,
                ["@cost"] = cost,
                ["@sale"] = sale,
                ["@stock"] = stock,
                ["@min"] = minStock
            };
            return conexion.ejecutarComandoParametrizado(sql, p) > 0;
        }

        public DataTable GetAllProducts()
        {
            return conexion.ejecutarConsulta("SELECT Id, Code, Name, CostPrice, SalePrice, Stock, MinStock FROM Products");
        }

        public bool InsertStockMovement(int productId, int quantity, string notes)
        {
            var p = new System.Collections.Generic.Dictionary<string, object>
            {
                ["@pid"] = productId,
                ["@qty"] = quantity,
                ["@type"] = quantity > 0 ? "IN" : "OUT",
                ["@notes"] = notes ?? ""
            };
            bool ok = conexion.ejecutarComandoParametrizado(
                "INSERT INTO StockMovements (ProductId, Quantity, Type, Notes) VALUES (@pid, @qty, @type, @notes)", p) > 0;
            if (ok)
            {
                conexion.ejecutarComandoParametrizado("UPDATE Products SET Stock = Stock + @qty WHERE Id=@pid", p);
            }
            return ok;
        }
    }
}

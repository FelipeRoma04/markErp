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

        public bool InsertProduct(string code, string name, decimal price, int stock, int minStock)
        {
            string query = $"INSERT INTO Products (Code, Name, Price, Stock, MinStock) VALUES ('{code}', '{name}', {price}, {stock}, {minStock})";
            return conexion.ejecutarSinRetornoDatos(query);
        }

        public DataTable GetAllProducts()
        {
            return conexion.ejecutarConsulta("SELECT * FROM Products");
        }
    }
}

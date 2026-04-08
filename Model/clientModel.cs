using System;

namespace Proyecto.Model
{
    internal class clientModel
    {
        conexionModel conexion;

        public clientModel()
        {
            conexion = new conexionModel();
        }

        public bool InsertClient(string documentId, string name, string email, string phone, string address, string city)
        {
            var sql = "INSERT INTO Clients (DocumentID, Name, Email, Phone, Address, City) VALUES (@doc, @name, @mail, @phone, @addr, @city)";
            var p = new System.Collections.Generic.Dictionary<string, object>
            {
                ["@doc"] = documentId,
                ["@name"] = name,
                ["@mail"] = email ?? "",
                ["@phone"] = phone ?? "",
                ["@addr"] = address ?? "",
                ["@city"] = city ?? ""
            };
            return conexion.ejecutarComandoParametrizado(sql, p) > 0;
        }
    }
}

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

        public bool InsertClient(string documentId, string name, string email, string phone, string address)
        {
            string query = $"INSERT INTO Clients (DocumentID, Name, Email, Phone, Address) VALUES ('{documentId}', '{name}', '{email}', '{phone}', '{address}')";
            return conexion.ejecutarSinRetornoDatos(query);
        }
    }
}

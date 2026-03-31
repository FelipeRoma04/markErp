using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Proyecto.Model
{
    internal class authModel
    {
        conexionModel conexion;

        public authModel()
        {
            conexion = new conexionModel();
        }

        public DataTable ValidarUsuario(string username, string password)
        {
            const string query = "SELECT Id, Username, Role, Password FROM Users WHERE Username = @username";
            var parameters = new Dictionary<string, object>
            {
                {"@username", username}
            };

            DataTable result = conexion.ejecutarConsultaParametrizada(query, parameters);
            if (result == null || result.Rows.Count == 0) return null;

            string storedHash = result.Rows[0]["Password"].ToString();
            string providedHash = ComputeSha256(password);

            if (string.Equals(storedHash, providedHash, StringComparison.OrdinalIgnoreCase))
            {
                // Return only essential columns
                DataTable filtered = result.DefaultView.ToTable(false, "Id", "Username", "Role");
                return filtered;
            }

            return null;
        }

        public bool RegistrarUsuario(string username, string password, string role = "Usuario")
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return false;

            const string query = "INSERT INTO Users (Username, Password, Role) VALUES (@username, @password, @role)";
            var parameters = new Dictionary<string, object>
            {
                {"@username", username},
                {"@password", ComputeSha256(password)},
                {"@role", role}
            };

            return conexion.ejecutarComandoParametrizado(query, parameters) > 0;
        }

        private string ComputeSha256(string raw)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(raw));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}

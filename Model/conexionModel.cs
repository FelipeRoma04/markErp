using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace Proyecto.Model
{
    internal class conexionModel
    {
        private string cadenaConexion = "Server=localhost\\SQLEXPRESS;Database=dbProyecto;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=15";
        SqlConnection conexion;

        public conexionModel() { }

        // Allow tests or config to inject a different connection string
        public conexionModel(string connectionString)
        {
            if (!string.IsNullOrWhiteSpace(connectionString))
                cadenaConexion = connectionString;
        }
        
        private string GetEffectiveConnectionString()
        {
            try
            {
                var cs = ConfigurationManager.ConnectionStrings["MarkErp"]?.ConnectionString;
                if (!string.IsNullOrWhiteSpace(cs)) return cs;
            }
            catch { /* ignore and use fallback */ }

            return this.cadenaConexion;
        }

        // Expose for debugging: returns the effective connection string used by the model
        public string ObtenerCadenaConexion()
        {
            return GetEffectiveConnectionString();
        }

        public SqlConnection establecerConexion()
        {
            this.conexion = new SqlConnection(GetEffectiveConnectionString());
            return this.conexion;
        }

        /// <summary>
        /// Tests the SQL Server connection and returns null when OK or the exception text when failing.
        /// </summary>
        public string ProbarConexion()
        {
            try
            {
                using (var testConn = new SqlConnection(GetEffectiveConnectionString()))
                {
                    testConn.Open();
                    testConn.Close();
                }
                return null;
            }
            catch (Exception ex)
            {
                try
                {
                    var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cliente-crash.log");
                    File.AppendAllText(logPath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ProbarConexion error: {ex}\n\n");
                }
                catch { }
                return ex.ToString();
            }
        }

        public bool ejecutarSinRetornoDatos(string strComando)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandText = strComando;
                comando.Connection = this.establecerConexion();
                conexion.Open();
                comando.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (Exception)
            {
                // Let caller decide how to handle failures; returning false previously hid issues.
                throw;
            }
        }

        public DataTable ejecutarConsulta(string strComando)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandText = strComando;
                comando.Connection = this.establecerConexion();
                conexion.Open();
                
                SqlDataAdapter configAdapter = new SqlDataAdapter(comando);
                DataTable tablaDeDatos = new DataTable();
                configAdapter.Fill(tablaDeDatos);
                
                conexion.Close();
                return tablaDeDatos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable ejecutarConsultaParametrizada(string query, Dictionary<string, object> parameters)
        {
            try
            {
                SqlCommand comando = new SqlCommand(query, this.establecerConexion());
                foreach (var param in parameters)
                {
                    comando.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }
                
                conexion.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataTable table = new DataTable();
                adapter.Fill(table);
                conexion.Close();
                return table;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int ejecutarComandoParametrizado(string query, Dictionary<string, object> parameters)
        {
            try
            {
                SqlCommand comando = new SqlCommand(query, this.establecerConexion());
                foreach (var param in parameters)
                {
                    comando.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }
                
                conexion.Open();
                int rowsAffected = comando.ExecuteNonQuery();
                conexion.Close();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ejecutarComandoSql(SqlCommand comando)
        {
            try
            {
                comando.Connection = this.establecerConexion();
                conexion.Open();
                comando.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

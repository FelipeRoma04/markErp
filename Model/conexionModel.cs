using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Proyecto.Model
{
    internal class conexionModel
    {
        private string cadenaConexion = "Server=.\\SQLEXPRESS;Database=markErp;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True";
        SqlConnection conexion;
        
        public SqlConnection establecerConexion()
        {
            this.conexion = new SqlConnection(this.cadenaConexion);
            return this.conexion;
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
            catch 
            {
                return false;   
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
            catch
            {
                return null;
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
            catch
            {
                return null;
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
            catch
            {
                return 0;
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
            catch
            {
                return false;
            }
        }
    }
}

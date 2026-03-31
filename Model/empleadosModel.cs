using System;
using System.Data;
using System.Data.SqlClient;

namespace Proyecto.Model
{
    internal class empleadosModel
    {
        conexionModel conexion;

        public empleadosModel()
        {
            conexion = new conexionModel();
        }

        public bool InsertarEmpleado(string name, string lastName, string secondName, string email, int departmentId, byte[] picFoto)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Empleados (Name, LastName, SecondName, Email, DepartmentId, PicFoto) VALUES (@name, @lastName, @secondName, @email, @departmentId, @picFoto)");
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@lastName", lastName);
            cmd.Parameters.AddWithValue("@secondName", (object)secondName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@email", (object)email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@departmentId", departmentId > 0 ? (object)departmentId : DBNull.Value);
            cmd.Parameters.Add("@picFoto", SqlDbType.VarBinary, -1).Value = (object)picFoto ?? DBNull.Value;
            
            return conexion.ejecutarComandoSql(cmd);
        }

        public bool ModificarEmpleado(int id, string name, string lastName, string secondName, string email, int departmentId, byte[] picFoto)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Empleados SET Name=@name, LastName=@lastName, SecondName=@secondName, Email=@email, DepartmentId=@departmentId, PicFoto=@picFoto WHERE Id=@id");
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@lastName", lastName);
            cmd.Parameters.AddWithValue("@secondName", (object)secondName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@email", (object)email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@departmentId", departmentId > 0 ? (object)departmentId : DBNull.Value);
            cmd.Parameters.Add("@picFoto", SqlDbType.VarBinary, -1).Value = (object)picFoto ?? DBNull.Value;

            return conexion.ejecutarComandoSql(cmd);
        }

        public bool BorrarEmpleado(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Empleados WHERE Id=@id");
            cmd.Parameters.AddWithValue("@id", id);
            return conexion.ejecutarComandoSql(cmd);
        }

        public DataTable ObtenerEmpleados()
        {
            return conexion.ejecutarConsulta("SELECT * FROM Empleados");
        }
    }
}

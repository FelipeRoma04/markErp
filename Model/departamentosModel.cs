using System.Collections.Generic;
using System.Data;
using Proyecto.Controler;

namespace Proyecto.Model
{
    internal class departamentosModel
    {
        conexionModel conexion;

        public departamentosModel()
        {

            conexion = new conexionModel();

        }

        public bool agregar(string nombreDepartamento)
        {
            const string query = "INSERT INTO Departamentos (departamento) VALUES (@nombre)";
            var parametros = new Dictionary<string, object>
            {
                {"@nombre", nombreDepartamento}
            };
            return conexion.ejecutarComandoParametrizado(query, parametros) > 0;
        }

        public bool actualizar(int id, string nombreDepartamento)
        {
            const string query = "UPDATE Departamentos SET departamento=@nombre WHERE Id=@id";
            var parametros = new Dictionary<string, object>
            {
                {"@id", id},
                {"@nombre", nombreDepartamento}
            };
            return conexion.ejecutarComandoParametrizado(query, parametros) > 0;
        }

        public bool eliminar(int id)
        {
            const string query = "DELETE FROM Departamentos WHERE Id=@id";
            var parametros = new Dictionary<string, object> { { "@id", id } };
            return conexion.ejecutarComandoParametrizado(query, parametros) > 0;
        }

        public DataTable listar()
        {
            const string query = "SELECT Id, departamento FROM Departamentos ORDER BY Id DESC";
            return conexion.ejecutarConsulta(query);
        }
    }
}

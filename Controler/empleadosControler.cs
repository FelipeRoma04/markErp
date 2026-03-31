using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Proyecto.Model;

namespace Proyecto.Controler
{
    internal class empleadosControler
    {
        public int id { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string secondName { get; set; }
        public int departamento { get; set; }
        public string email { get; set; }
        public byte[] picFoto { get; set; }

        public bool agregar()
        {
            empleadosModel model = new empleadosModel();
            bool ok = model.InsertarEmpleado(name, lastName, secondName, email, departamento, picFoto);
            if (ok) AuditControler.Log("CREATE", "Empleados", name, $"Empleado {name} {lastName} creado");
            return ok;
        }

        public bool modificar()
        {
            empleadosModel model = new empleadosModel();
            bool ok = model.ModificarEmpleado(id, name, lastName, secondName, email, departamento, picFoto);
            if (ok) AuditControler.Log("UPDATE", "Empleados", id.ToString(), $"Empleado ID {id} modificado");
            return ok;
        }

        public bool eliminar()
        {
            empleadosModel model = new empleadosModel();
            bool ok = model.BorrarEmpleado(id);
            if (ok) AuditControler.Log("DELETE", "Empleados", id.ToString(), $"Empleado ID {id} eliminado");
            return ok;
        }

        public DataTable listar()
        {
            empleadosModel model = new empleadosModel();
            return model.ObtenerEmpleados();
        }
    }
}

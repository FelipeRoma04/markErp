using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto.Model;
namespace Proyecto.Controler
{
    internal class departamentoControler
    {
        public int id { get; set; }
        public string departamento { get; set; }
        
        public bool agregar() 
        {
            departamentosModel model = new departamentosModel();
            return model.agregar(this.departamento);
        }

        public bool modificar()
        {
            departamentosModel model = new departamentosModel();
            return model.actualizar(this.id, this.departamento);
        }

        public bool eliminar()
        {
            departamentosModel model = new departamentosModel();
            return model.eliminar(this.id);
        }

        public System.Data.DataTable listar()
        {
            departamentosModel model = new departamentosModel();
            return model.listar();
        }
    }
}

using System;
using System.Data;
using Proyecto.Model;

namespace Proyecto.Controler
{
    internal class productControler
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int MinStock { get; set; }

        public bool IsLowStock 
        {
            get { return Stock < MinStock; }
        }

        public bool AddProduct()
        {
            if (!ValidationHelper.IsNotEmpty(Code) || !ValidationHelper.IsNotEmpty(Name))
            {
                ValidationHelper.ShowValidationError("Código y Nombre son obligatorios.");
                return false;
            }

            productModel model = new productModel();
            return model.InsertProduct(Code, Name, Price, Stock, MinStock);
        }

        public DataTable GetInventory()
        {
            productModel model = new productModel();
            return model.GetAllProducts();
        }
    }
}

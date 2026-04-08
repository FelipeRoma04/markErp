using System;
using Proyecto.Model;

namespace Proyecto.Controler
{
    internal class clientControler
    {
        public string DocumentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public bool AddClient()
        {
            // Internal safety validation
            if (!ValidationHelper.IsNotEmpty(Name) || !ValidationHelper.IsNotEmpty(DocumentId))
            {
                ValidationHelper.ShowValidationError("Documento y Nombre son campos obligatorios.");
                return false;
            }

            if (!string.IsNullOrEmpty(Email) && !ValidationHelper.IsValidEmail(Email))
            {
                ValidationHelper.ShowValidationError("Por favor, ingrese un email con formato correcto.");
                return false;
            }

            clientModel model = new clientModel();
            return model.InsertClient(DocumentId, Name, Email, Phone, Address, City);
        }
    }
}

using System;
using System.Data;
using Proyecto.Model;

namespace Proyecto.Controler
{
    internal class assetControler
    {
        public int AssetId { get; set; }
        
        // Basic properties
        public string SerialNumber { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }

        // FASE 3 Properties
        public decimal PurchasePrice { get; set; }
        public decimal SalvageValue { get; set; }
        public int UsefulLifeYears { get; set; }
        public int LocationId { get; set; }

        // Assignment properties
        public int EmployeeId { get; set; }
        public DateTime AssignDate { get; set; }

        public bool CreateAsset()
        {
            if (!ValidationHelper.IsNotEmpty(SerialNumber)) {
                ValidationHelper.ShowValidationError("El código serial es obligatorio.");
                return false;
            }
            assetModel model = new assetModel();
            bool ok = model.InsertAsset(SerialNumber, Type, Brand, PurchasePrice, SalvageValue, UsefulLifeYears, LocationId);
            if (ok) AuditControler.Log("CREATE", "Assets", SerialNumber, $"Activo {SerialNumber} ({Type}) registrado");
            return ok;
        }

        public bool BindAssetToEmployee()
        {
            if (EmployeeId <= 0 || AssetId <= 0) {
               ValidationHelper.ShowValidationError("Ingresa IDs numéricos válidos.");
               return false; 
            }
            assetModel model = new assetModel();
            bool ok2 = model.AssignAsset(AssetId, EmployeeId, AssignDate);
            if (ok2) AuditControler.Log("UPDATE", "Assets", AssetId.ToString(), $"Activo {AssetId} asignado al empleado {EmployeeId}");
            return ok2;
        }

        public DataTable ConsultHistory()
        {
            assetModel model = new assetModel();
            return model.GetAssetHistory(AssetId);
        }

        // FASE 3 Operations
        public DataTable ConsultarActivos()
        {
            assetModel model = new assetModel();
            return model.ConsultarActivos();
        }

        public DataTable ObtenerSedes()
        {
            assetModel model = new assetModel();
            return model.ObtenerSedes();
        }

        public bool ScheduleMaintenance(string description, DateTime date)
        {
            if (AssetId <= 0 || !ValidationHelper.IsNotEmpty(description)) {
                ValidationHelper.ShowValidationError("ID del Asset y la Descripción son campos requeridos.");
                return false;
            }
            assetModel model = new assetModel();
            return model.ScheduleMaintenance(AssetId, description, date);
        }

        public DataTable ConsultMaintenance()
        {
            assetModel model = new assetModel();
            return model.GetMaintenance(AssetId);
        }

        // Depreciación por línea recta
        public string CalculateCurrentValue(decimal originalPurchase, decimal salvage, int lifeYears, DateTime purchaseDate)
        {
            if (lifeYears <= 0) return "No depreciable";

            int yearsPassed = DateTime.Now.Year - purchaseDate.Year;
            if (DateTime.Now.Month < purchaseDate.Month || (DateTime.Now.Month == purchaseDate.Month && DateTime.Now.Day < purchaseDate.Day))
            {
                yearsPassed--; // Exact full years
            }
            if (yearsPassed < 0) yearsPassed = 0;

            if (yearsPassed >= lifeYears) return salvage.ToString("C");

            decimal deprecationPerYear = (originalPurchase - salvage) / lifeYears;
            decimal currentValue = originalPurchase - (deprecationPerYear * yearsPassed);
            
            return currentValue.ToString("C");
        }
    }
}

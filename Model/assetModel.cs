using System;
using System.Data;

namespace Proyecto.Model
{
    internal class assetModel
    {
        conexionModel conexion;

        public assetModel()
        {
            conexion = new conexionModel();
        }

        public bool InsertAsset(string serial, string type, string brand, decimal purchasePrice, decimal salvageValue, int usefulLife, int locationId)
        {
            // Set LocationId explicitly to NULL in SQL if it's 0 (meaning not selected)
            string locIdParam = locationId > 0 ? locationId.ToString() : "NULL";
            
            string q = $"INSERT INTO Assets (SerialNumber, Type, Brand, Status, PurchasePrice, SalvageValue, UsefulLifeYears, PurchaseDate, LocationId) " +
                       $"VALUES ('{serial}', '{type}', '{brand}', 'Available', {purchasePrice}, {salvageValue}, {usefulLife}, GETDATE(), {locIdParam})";
            return conexion.ejecutarSinRetornoDatos(q);
        }

        public bool AssignAsset(int assetId, int employeeId, DateTime date)
        {
            string q1 = $"INSERT INTO AssetAssignments (AssetId, EmployeeId, AssignDate) VALUES ({assetId}, {employeeId}, '{date:yyyy-MM-dd}')";
            string q2 = $"UPDATE Assets SET Status = 'Assigned' WHERE Id = {assetId}";
            conexion.ejecutarSinRetornoDatos(q2); 
            return conexion.ejecutarSinRetornoDatos(q1); 
        }

        public DataTable GetAssetHistory(int assetId)
        {
            return conexion.ejecutarConsulta($"SELECT * FROM AssetAssignments WHERE AssetId = {assetId}");
        }

        public DataTable ConsultarActivos()
        {
            return conexion.ejecutarConsulta("SELECT * FROM Assets");
        }

        public DataTable ObtenerSedes()
        {
            return conexion.ejecutarConsulta("SELECT * FROM Locations");
        }

        public bool ScheduleMaintenance(int assetId, string description, DateTime date)
        {
            string q = $"INSERT INTO MaintenanceSchedule (AssetId, Description, ScheduledDate, IsCompleted) VALUES ({assetId}, '{description}', '{date:yyyy-MM-dd}', 0)";
            return conexion.ejecutarSinRetornoDatos(q);
        }

        public DataTable GetMaintenance(int assetId)
        {
            return conexion.ejecutarConsulta($"SELECT * FROM MaintenanceSchedule WHERE AssetId = {assetId}");
        }
    }
}

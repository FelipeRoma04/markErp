using System;
using System.Data;

namespace Proyecto.Model
{
    internal class auditModel
    {
        conexionModel conexion;

        public auditModel()
        {
            conexion = new conexionModel();
        }

        public bool Log(string userLogin, string actionType, string tableName, string recordId, string description)
        {
            string q = $"INSERT INTO AuditLogs (UserLogin, ActionType, TableName, RecordId, Description, CreatedAt) " +
                       $"VALUES ('{userLogin}', '{actionType}', '{tableName}', '{recordId}', '{description.Replace("'", "''")}', GETDATE())";
            return conexion.ejecutarSinRetornoDatos(q);
        }

        public DataTable GetLogs(string tableName = null, string actionType = null, DateTime? from = null)
        {
            string where = "WHERE 1=1";
            if (!string.IsNullOrEmpty(tableName))  where += $" AND TableName = '{tableName}'";
            if (!string.IsNullOrEmpty(actionType)) where += $" AND ActionType = '{actionType}'";
            if (from.HasValue)                     where += $" AND CreatedAt >= '{from.Value:yyyy-MM-dd}'";

            return conexion.ejecutarConsulta(
                $"SELECT Id, UserLogin, ActionType, TableName, RecordId, Description, CreatedAt FROM AuditLogs {where} ORDER BY CreatedAt DESC");
        }
    }
}

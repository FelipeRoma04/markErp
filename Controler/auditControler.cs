using System;
using System.Data;
using Proyecto.Model;

namespace Proyecto.Controler
{
    /// <summary>
    /// Static auditing helper — call AuditControler.Log() right after any successful CRUD operation.
    /// Uses UserSession.Current to resolve the logged-in user.
    /// </summary>
    public static class AuditControler
    {
        public static void Log(string actionType, string tableName, string recordId, string description)
        {
            try
            {
                string user = UserSession.Current ?? "sistema";
                auditModel model = new auditModel();
                model.Log(user, actionType, tableName, recordId, description);
            }
            catch { /* Audit must never crash the app */ }
        }

        public static DataTable GetLogs(string tableName = null, string actionType = null, DateTime? from = null)
        {
            auditModel model = new auditModel();
            return model.GetLogs(tableName, actionType, from);
        }
    }
}

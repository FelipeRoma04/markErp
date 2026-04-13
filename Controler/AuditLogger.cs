using System;
using System.Collections.Generic;

namespace Proyecto.Controler
{
    /// <summary>
    /// Task 24: AuditLogger - Logs all CRUD operations for security and compliance
    /// </summary>
    public static class AuditLogger
    {
        public enum ActionType
        {
            INSERT,
            UPDATE,
            DELETE,
            LOGIN,
            LOGOUT,
            EXPORT,
            OTHER
        }

        /// <summary>
        /// Log an action to the AuditLogs table
        /// </summary>
        public static bool Log(string userLogin, ActionType action, string tableName, string recordId, string description)
        {
            try
            {
                // If no user is logged in, use 'sistema'
                if (string.IsNullOrWhiteSpace(userLogin))
                    userLogin = UserSession.Username ?? "sistema";

                return LogToDatabase(userLogin, action.ToString(), tableName, recordId, description);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(
                    System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "audit-error.log"),
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Error logging action: {ex.Message}\n"
                );
                return false;
            }
        }

        private static bool LogToDatabase(string userLogin, string actionType, string tableName, string recordId, string description)
        {
            try
            {
                Model.conexionModel conexion = new Model.conexionModel();
                
                const string query = @"
                    INSERT INTO AuditLogs (UserLogin, ActionType, TableName, RecordId, Description, CreatedAt)
                    VALUES (@user, @action, @table, @recordId, @desc, GETDATE())";

                var parameters = new Dictionary<string, object>
                {
                    ["@user"] = userLogin,
                    ["@action"] = actionType,
                    ["@table"] = tableName,
                    ["@recordId"] = recordId ?? "",
                    ["@desc"] = description ?? ""
                };

                return conexion.ejecutarComandoParametrizado(query, parameters) > 0;
            }
            catch
            {
                return false;
            }
        }

        // Helper methods for common operations
        public static void LogInsert(string tableName, string recordId, string description)
        {
            Log(null, ActionType.INSERT, tableName, recordId, description);
        }

        public static void LogUpdate(string tableName, string recordId, string description)
        {
            Log(null, ActionType.UPDATE, tableName, recordId, description);
        }

        public static void LogDelete(string tableName, string recordId, string description)
        {
            Log(null, ActionType.DELETE, tableName, recordId, description);
        }

        public static void LogUpdateWithDelta(string tableName, string recordId, Dictionary<string, object> oldValues, Dictionary<string, object> newValues)
        {
            string delta = CalculateDelta(oldValues, newValues);
            Log(null, ActionType.UPDATE, tableName, recordId, $"Cambios detectados: {delta}");
        }

        private static string CalculateDelta(Dictionary<string, object> oldValues, Dictionary<string, object> newValues)
        {
            if (oldValues == null || newValues == null) return "Sin datos de comparación.";

            List<string> changes = new List<string>();
            foreach (var key in newValues.Keys)
            {
                if (oldValues.ContainsKey(key))
                {
                    var oldVal = oldValues[key]?.ToString() ?? "NULL";
                    var newVal = newValues[key]?.ToString() ?? "NULL";

                    if (oldVal != newVal)
                    {
                        changes.Add($"{key}: '{oldVal}' → '{newVal}'");
                    }
                }
                else
                {
                    changes.Add($"{key}: [NUEVO] '{newValues[key]}'");
                }
            }

            return changes.Count > 0 ? string.Join(", ", changes) : "Sin cambios significativos.";
        }

        public static void LogLogin(string username)
        {
            Log(username, ActionType.LOGIN, "Users", username, $"Usuario {username} inició sesión");
        }

        public static void LogLogout(string username)
        {
            Log(username, ActionType.LOGOUT, "Users", username, $"Usuario {username} cerró sesión");
        }

        public static void LogExport(string tableName, string description)
        {
            Log(null, ActionType.EXPORT, tableName, "", description);
        }
    }
}

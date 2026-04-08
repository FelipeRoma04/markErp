using Proyecto.Api.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Api.Services
{
    public static class AuditHelper
    {
        public static Task LogAsync(DbService db, string user, string action, string table, string recordId, string description = null)
        {
            // In in-memory mode DbService.ExecuteAsync will just return true and skip DB.
            return db.ExecuteAsync(
                "INSERT INTO AuditLogs (UserLogin, ActionType, TableName, RecordId, Description) VALUES (@user, @action, @table, @id, @desc)",
                new Dictionary<string, object>
                {
                    ["@user"] = string.IsNullOrWhiteSpace(user) ? "api" : user,
                    ["@action"] = action,
                    ["@table"] = table,
                    ["@id"] = recordId ?? string.Empty,
                    ["@desc"] = description ?? string.Empty
                });
        }
    }
}

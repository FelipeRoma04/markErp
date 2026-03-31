using Microsoft.AspNetCore.Mvc;
using Proyecto.Api.Data;
using System.Collections.Generic;

namespace Proyecto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditController : ControllerBase
    {
        private readonly DbService _db;

        public AuditController(DbService db) => _db = db;

        // GET /api/audit
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string tableName = null, [FromQuery] string actionType = null)
        {
            string where = "WHERE 1=1";
            var p = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(tableName))
            {
                where += " AND TableName = @tbl";
                p["@tbl"] = tableName;
            }

            if (!string.IsNullOrEmpty(actionType))
            {
                where += " AND ActionType = @act";
                p["@act"] = actionType;
            }

            var dt = await _db.QueryAsync(
                $"SELECT * FROM AuditLogs {where} ORDER BY CreatedAt DESC", p);
            return Ok(DbService.ToList(dt));
        }

        // GET /api/audit/by-user/{username}
        [HttpGet("by-user/{username}")]
        public async Task<IActionResult> GetByUser(string username)
        {
            var dt = await _db.QueryAsync(
                "SELECT * FROM AuditLogs WHERE UserLogin = @user ORDER BY CreatedAt DESC",
                new Dictionary<string, object> { ["@user"] = username });

            return Ok(DbService.ToList(dt));
        }

        // GET /api/audit/by-table/{tableName}
        [HttpGet("by-table/{tableName}")]
        public async Task<IActionResult> GetByTable(string tableName)
        {
            var dt = await _db.QueryAsync(
                "SELECT * FROM AuditLogs WHERE TableName = @tbl ORDER BY CreatedAt DESC",
                new Dictionary<string, object> { ["@tbl"] = tableName });

            return Ok(DbService.ToList(dt));
        }

        // GET /api/audit/record/{tableName}/{recordId}
        [HttpGet("record/{tableName}/{recordId}")]
        public async Task<IActionResult> GetRecordHistory(string tableName, string recordId)
        {
            var dt = await _db.QueryAsync(
                "SELECT * FROM AuditLogs WHERE TableName = @tbl AND RecordId = @rid ORDER BY CreatedAt DESC",
                new Dictionary<string, object> { ["@tbl"] = tableName, ["@rid"] = recordId });

            return Ok(DbService.ToList(dt));
        }
    }
}

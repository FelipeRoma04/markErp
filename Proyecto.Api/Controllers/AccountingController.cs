using Microsoft.AspNetCore.Mvc;
using Proyecto.Api.Data;
using System.Collections.Generic;

namespace Proyecto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountingController : ControllerBase
    {
        private readonly DbService _db;

        public AccountingController(DbService db) => _db = db;

        // GET /api/accounting/accounts
        [HttpGet("accounts")]
        public async Task<IActionResult> GetAllAccounts([FromQuery] string accountType = null)
        {
            string where = "WHERE 1=1";
            var p = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(accountType))
            {
                where += " AND AccountType = @type";
                p["@type"] = accountType;
            }

            var dt = await _db.QueryAsync(
                $"SELECT * FROM PUC_Accounts {where} ORDER BY Code", p);
            return Ok(DbService.ToList(dt));
        }

        // GET /api/accounting/accounts/{id}
        [HttpGet("accounts/{id:int}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var dt = await _db.QueryAsync(
                "SELECT * FROM PUC_Accounts WHERE Id = @id",
                new Dictionary<string, object> { ["@id"] = id });

            if (dt.Rows.Count == 0) return NotFound();
            return Ok(DbService.ToList(dt)[0]);
        }

        // GET /api/accounting/journal
        [HttpGet("journal")]
        public async Task<IActionResult> GetJournal()
        {
            var dt = await _db.QueryAsync(
                "SELECT * FROM JournalEntries ORDER BY EntryDate DESC, Id DESC", new());
            return Ok(DbService.ToList(dt));
        }

        // GET /api/accounting/journal/{id}
        [HttpGet("journal/{id:int}")]
        public async Task<IActionResult> GetJournalById(int id)
        {
            var dt = await _db.QueryAsync(
                "SELECT je.*, COUNT(jl.Id) as LineCount FROM JournalEntries je " +
                "LEFT JOIN JournalLines jl ON je.Id = jl.JournalEntryId " +
                "WHERE je.Id = @id GROUP BY je.Id, je.EntryDate, je.Description, je.CreatedBy, je.CreatedAt",
                new Dictionary<string, object> { ["@id"] = id });

            if (dt.Rows.Count == 0) return NotFound();
            return Ok(DbService.ToList(dt)[0]);
        }

        // POST /api/accounting/journal
        [HttpPost("journal")]
        public async Task<IActionResult> CreateJournalEntry([FromBody] CreateJournalRequest req)
        {
            if (req.EntryDate == default || string.IsNullOrWhiteSpace(req.Description))
                return BadRequest("EntryDate y Description requeridos.");

            bool ok = await _db.ExecuteAsync(
                "INSERT INTO JournalEntries (EntryDate, Description, CreatedBy, CreatedAt) " +
                "VALUES (@date, @desc, @user, GETDATE())",
                new Dictionary<string, object>
                {
                    ["@date"] = req.EntryDate,
                    ["@desc"] = req.Description,
                    ["@user"] = req.CreatedBy ?? "api"
                });

            return ok ? Ok(new { message = "Asiento creado" }) : BadRequest();
        }

        // POST /api/accounting/journal-lines
        [HttpPost("journal-lines")]
        public async Task<IActionResult> CreateJournalLine([FromBody] CreateJournalLineRequest req)
        {
            if (req.JournalEntryId <= 0 || req.AccountId <= 0)
                return BadRequest("JournalEntryId y AccountId requeridos.");

            bool ok = await _db.ExecuteAsync(
                "INSERT INTO JournalLines (JournalEntryId, AccountId, Debit, Credit) " +
                "VALUES (@jId, @aId, @debit, @credit)",
                new Dictionary<string, object>
                {
                    ["@jId"] = req.JournalEntryId,
                    ["@aId"] = req.AccountId,
                    ["@debit"] = req.Debit,
                    ["@credit"] = req.Credit
                });

            return ok ? Ok(new { message = "Línea agregada" }) : BadRequest();
        }

        // GET /api/accounting/balance-sheet
        [HttpGet("balance-sheet")]
        public async Task<IActionResult> GetBalanceSheet([FromQuery] DateTime? asOfDate = null)
        {
            string dateFilter = asOfDate.HasValue ? $"WHERE CreatedAt <= '{asOfDate:yyyy-MM-dd}'" : "";
            var dt = await _db.QueryAsync(
                $@"SELECT pa.Code, pa.Name, pa.AccountType,
                    SUM(CASE WHEN jl.Debit > 0 THEN jl.Debit ELSE -jl.Credit END) as Balance
                    FROM JournalLines jl
                    JOIN PUC_Accounts pa ON jl.AccountId = pa.Id
                    JOIN JournalEntries je ON jl.JournalEntryId = je.Id {dateFilter}
                    GROUP BY pa.Code, pa.Name, pa.AccountType
                    ORDER BY pa.Code", new());
            return Ok(DbService.ToList(dt));
        }

        public class CreateJournalRequest
        {
            public DateTime EntryDate { get; set; }
            public string Description { get; set; }
            public string CreatedBy { get; set; }
        }

        public class CreateJournalLineRequest
        {
            public int JournalEntryId { get; set; }
            public int AccountId { get; set; }
            public decimal Debit { get; set; }
            public decimal Credit { get; set; }
        }
    }
}

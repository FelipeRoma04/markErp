using System;
using System.Data;

namespace Proyecto.Model
{
    internal class accountingModel
    {
        conexionModel conexion;

        public accountingModel()
        {
            conexion = new conexionModel();
        }

        // ─── PUC Accounts ───────────────────────────────
        public DataTable GetAccounts(string filter = null)
        {
            string where = string.IsNullOrEmpty(filter) ? "" : $"WHERE Code LIKE '%{filter}%' OR Name LIKE '%{filter}%'";
            return conexion.ejecutarConsulta($"SELECT * FROM PUC_Accounts {where} ORDER BY Code");
        }

        // ─── Journal Entries ────────────────────────────
        public int InsertJournalEntry(DateTime date, string description, string createdBy)
        {
            // Insert and return the new ID
            string q = $"INSERT INTO JournalEntries (EntryDate, Description, CreatedBy) " +
                       $"VALUES ('{date:yyyy-MM-dd}', '{description.Replace("'", "''")}', '{createdBy}'); " +
                       $"SELECT SCOPE_IDENTITY();";
            
            DataTable result = conexion.ejecutarConsulta(q);
            if (result != null && result.Rows.Count > 0)
                return Convert.ToInt32(result.Rows[0][0]);
            return -1;
        }

        public bool InsertJournalLine(int journalEntryId, int accountId, decimal debit, decimal credit)
        {
            string q = $"INSERT INTO JournalLines (JournalEntryId, AccountId, Debit, Credit) " +
                       $"VALUES ({journalEntryId}, {accountId}, {debit}, {credit})";
            return conexion.ejecutarSinRetornoDatos(q);
        }

        public DataTable GetJournalEntries(DateTime? from = null, DateTime? to = null)
        {
            string where = "WHERE 1=1";
            if (from.HasValue) where += $" AND je.EntryDate >= '{from.Value:yyyy-MM-dd}'";
            if (to.HasValue)   where += $" AND je.EntryDate <= '{to.Value:yyyy-MM-dd}'";

            return conexion.ejecutarConsulta(
                $"SELECT je.Id, je.EntryDate, je.Description, je.CreatedBy, " +
                $"SUM(jl.Debit) AS TotalDebito, SUM(jl.Credit) AS TotalCredito " +
                $"FROM JournalEntries je LEFT JOIN JournalLines jl ON je.Id = jl.JournalEntryId " +
                $"{where} GROUP BY je.Id, je.EntryDate, je.Description, je.CreatedBy ORDER BY je.EntryDate DESC");
        }

        public DataTable GetJournalLines(int journalEntryId)
        {
            return conexion.ejecutarConsulta(
                $"SELECT jl.Id, pa.Code, pa.Name, jl.Debit, jl.Credit " +
                $"FROM JournalLines jl INNER JOIN PUC_Accounts pa ON jl.AccountId = pa.Id " +
                $"WHERE jl.JournalEntryId = {journalEntryId}");
        }

        // ─── Balance General (simplified) ──────────────
        public DataTable GetBalanceGeneral()
        {
            return conexion.ejecutarConsulta(
                "SELECT pa.AccountType, pa.Code, pa.Name, " +
                "SUM(jl.Debit) AS TotalDebito, SUM(jl.Credit) AS TotalCredito, " +
                "CASE WHEN pa.Nature='DEBITO' THEN SUM(jl.Debit)-SUM(jl.Credit) " +
                "     ELSE SUM(jl.Credit)-SUM(jl.Debit) END AS Saldo " +
                "FROM PUC_Accounts pa LEFT JOIN JournalLines jl ON pa.Id = jl.AccountId " +
                "GROUP BY pa.AccountType, pa.Code, pa.Name, pa.Nature " +
                "ORDER BY pa.Code");
        }

        // ─── P&L (Estado de Resultados) ─────────────────
        public DataTable GetPL(DateTime? from = null, DateTime? to = null)
        {
            string dateFilter = "WHERE je.EntryDate IS NOT NULL";
            if (from.HasValue) dateFilter += $" AND je.EntryDate >= '{from.Value:yyyy-MM-dd}'";
            if (to.HasValue)   dateFilter += $" AND je.EntryDate <= '{to.Value:yyyy-MM-dd}'";

            return conexion.ejecutarConsulta(
                "SELECT pa.AccountType, pa.Code, pa.Name, " +
                "SUM(CASE WHEN pa.Nature='CREDITO' THEN jl.Credit-jl.Debit ELSE jl.Debit-jl.Credit END) AS Saldo " +
                "FROM PUC_Accounts pa " +
                "INNER JOIN JournalLines jl ON pa.Id = jl.AccountId " +
                "INNER JOIN JournalEntries je ON jl.JournalEntryId = je.Id " +
                $"{dateFilter} " +
                "AND pa.AccountType IN ('INGRESO','GASTO','COSTO') " +
                "GROUP BY pa.AccountType, pa.Code, pa.Name, pa.Nature " +
                "ORDER BY pa.Code");
        }
    }
}

using System;
using System.Data;
using Proyecto.Model;

namespace Proyecto.Controler
{
    internal class accountingControler
    {
        // Journal entry properties
        public DateTime EntryDate { get; set; } = DateTime.Today;
        public string Description { get; set; }
        public int JournalEntryId { get; set; }
        public int AccountId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }

        // ─── PUC ───────────────────────────────────────
        public DataTable GetAccounts(string filter = null)
        {
            return new accountingModel().GetAccounts(filter);
        }

        // ─── Journal ───────────────────────────────────
        public int CreateJournalEntry()
        {
            if (string.IsNullOrWhiteSpace(Description))
            {
                ValidationHelper.ShowValidationError("La descripción del asiento es obligatoria.");
                return -1;
            }
            string user = UserSession.Current;
            int id = new accountingModel().InsertJournalEntry(EntryDate, Description, user);
            if (id > 0)
                AuditControler.Log("CREATE", "JournalEntries", id.ToString(), $"Asiento contable: {Description}");
            return id;
        }

        public bool AddLine()
        {
            if (JournalEntryId <= 0 || AccountId <= 0)
            {
                ValidationHelper.ShowValidationError("Selecciona un asiento y una cuenta contable.");
                return false;
            }
            return new accountingModel().InsertJournalLine(JournalEntryId, AccountId, Debit, Credit);
        }

        public DataTable GetJournalEntries(DateTime? from = null, DateTime? to = null)
        {
            return new accountingModel().GetJournalEntries(from, to);
        }

        public DataTable GetJournalLines()
        {
            return new accountingModel().GetJournalLines(JournalEntryId);
        }

        // ─── Reports ───────────────────────────────────
        public DataTable GetBalanceGeneral()
        {
            return new accountingModel().GetBalanceGeneral();
        }

        public DataTable GetPL(DateTime? from = null, DateTime? to = null)
        {
            return new accountingModel().GetPL(from, to);
        }
    }
}

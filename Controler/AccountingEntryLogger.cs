using System;
using System.Collections.Generic;
using System.Data;
using Proyecto.Model;

namespace Proyecto.Controler
{
    /// <summary>
    /// Tasks 28-29: Automatic accounting entries from sales transactions
    /// Handles: Invoice creation, Payment processing
    /// </summary>
    public static class AccountingEntryLogger
    {
        public enum AccountingAction
        {
            INVOICE_SALE,    // Task 28: Factura genera asiento
            INVOICE_CREDIT,  // Sale on credit
            PAYMENT_RECEIVED, // Task 29: Pago CXC
            PAYMENT_DISCOUNT, // With discount
            PAYMENT_REVERSAL  // Payment reversal
        }

        /// <summary>
        /// Task 28: Auto-generate accounting entry when invoice is issued
        /// Débito Clientes (1305), Crédito Ingresos (4135), Crédito IVA (2408)
        /// </summary>
        public static bool LogInvoiceEntry(int invoiceId, int clientId, decimal subtotal, decimal taxAmount, decimal total)
        {
            try
            {
                conexionModel conexion = new conexionModel();
                string description = $"Venta a cliente {clientId}, Factura #{invoiceId}";
                
                // Get next journal entry ID or create one
                const string createEntryQuery = @"
                    INSERT INTO JournalEntries (EntryDate, Description, CreatedBy, CreatedAt)
                    VALUES (GETDATE(), @desc, @user, GETDATE());
                    SELECT SCOPE_IDENTITY();";

                var entryParams = new Dictionary<string, object>
                {
                    ["@desc"] = description,
                    ["@user"] = UserSession.Username ?? "sistema"
                };

                var entryDt = conexion.ejecutarConsultaParametrizada(createEntryQuery, entryParams);
                if (entryDt == null || entryDt.Rows.Count == 0) return false;

                int journalEntryId = Convert.ToInt32(entryDt.Rows[0][0]);

                // Create debit entries and credit entries
                // Debit: Clientes 1305
                CreateJournalLineItem(conexion, journalEntryId, "1305", "DEBITO", total);
                
                // Credit: Ingresos por Ventas 4135
                CreateJournalLineItem(conexion, journalEntryId, "4135", "CREDITO", subtotal);
                
                // Credit: IVA por Pagar 2408
                CreateJournalLineItem(conexion, journalEntryId, "2408", "CREDITO", taxAmount);

                return true;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(
                    System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "accounting-error.log"),
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Error logging invoice entry: {ex.Message}\n");
                return false;
            }
        }

        /// <summary>
        /// Task 29: Auto-generate accounting entry when payment is received
        /// Débito Caja/Bancos, Crédito Clientes
        /// </summary>
        public static bool LogPaymentEntry(int invoiceId, int paymentId, decimal paymentAmount, string paymentMethod)
        {
            try
            {
                conexionModel conexion = new conexionModel();
                string description = $"Pago recibido por Factura #{invoiceId}, Método: {paymentMethod}";
                
                const string createEntryQuery = @"
                    INSERT INTO JournalEntries (EntryDate, Description, CreatedBy, CreatedAt)
                    VALUES (GETDATE(), @desc, @user, GETDATE());
                    SELECT SCOPE_IDENTITY();";

                var entryParams = new Dictionary<string, object>
                {
                    ["@desc"] = description,
                    ["@user"] = UserSession.Username ?? "sistema"
                };

                var entryDt = conexion.ejecutarConsultaParametrizada(createEntryQuery, entryParams);
                if (entryDt == null || entryDt.Rows.Count == 0) return false;

                int journalEntryId = Convert.ToInt32(entryDt.Rows[0][0]);

                // Debit: Caja General (1105) - Could vary based on payment method
                CreateJournalLineItem(conexion, journalEntryId, "1105", "DEBITO", paymentAmount);
                
                // Credit: Clientes (1305)
                CreateJournalLineItem(conexion, journalEntryId, "1305", "CREDITO", paymentAmount);

                return true;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(
                    System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "accounting-error.log"),
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Error logging payment entry: {ex.Message}\n");
                return false;
            }
        }

        private static bool CreateJournalLineItem(conexionModel conexion, int journalEntryId, string accountCode, string debitOrCredit, decimal amount)
        {
            try
            {
                // Get account ID from PUC_Accounts by code
                const string accountQuery = "SELECT Id FROM PUC_Accounts WHERE Code = @code";
                var accountParams = new Dictionary<string, object> { ["@code"] = accountCode };
                DataTable accountDt = conexion.ejecutarConsultaParametrizada(accountQuery, accountParams);
                
                if (accountDt == null || accountDt.Rows.Count == 0)
                    return false;

                int accountId = Convert.ToInt32(accountDt.Rows[0]["Id"]);

                // Insert journal line item
                const string insertQuery = @"
                    INSERT INTO JournalLines (JournalEntryId, AccountId, Debit, Credit)
                    VALUES (@entryId, @acctId, @debit, @credit)";

                var lineParams = new Dictionary<string, object>
                {
                    ["@entryId"] = journalEntryId,
                    ["@acctId"] = accountId,
                    ["@debit"] = (debitOrCredit == "DEBITO" ? amount : 0),
                    ["@credit"] = (debitOrCredit == "CREDITO" ? amount : 0)
                };

                return conexion.ejecutarComandoParametrizado(insertQuery, lineParams) > 0;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(
                    System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "accounting-error.log"),
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Error creating journal line: {ex.Message}\n");
                return false;
            }
        }
    }
}

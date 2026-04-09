using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Proyecto.Model;

namespace Proyecto.Controler
{
    /// <summary>
    /// Task 32: Financial Reports - Balance Sheet and Income Statement
    /// Queries JournalEntries (GL) to generate standardized financial reports
    /// Compliant with Colombian accounting standards (PUC - Plan Único de Cuentas)
    /// </summary>
    public static class FinancialReportGenerator
    {
        public enum AccountType
        {
            ACTIVO_CIRCULANTE,      // Current Assets
            ACTIVO_NO_CIRCULANTE,   // Non-Current Assets
            PASIVO_CIRCULANTE,      // Current Liabilities
            PASIVO_NO_CIRCULANTE,   // Non-Current Liabilities
            PATRIMONIO,             // Equity
            INGRESOS,               // Revenue
            GASTOS,                 // Expenses
            COSTO_VENTAS            // Cost of Sales
        }

        public class BalanceSheetLine
        {
            public string AccountCode { get; set; }
            public string AccountName { get; set; }
            public AccountType Type { get; set; }
            public decimal Amount { get; set; }
        }

        public class IncomeStatementLine
        {
            public string AccountCode { get; set; }
            public string AccountName { get; set; }
            public AccountType Type { get; set; }
            public decimal Amount { get; set; }
        }

        /// <summary>
        /// Task 32: Generate Balance Sheet for a given date
        /// Shows Assets, Liabilities, and Equity per Colombian PUC standards
        /// </summary>
        public static DataTable GenerateBalanceSheet(DateTime asOfDate)
        {
            try
            {
                conexionModel conexion = new conexionModel();

                const string query = @"
                    -- Balance Sheet grouped by account type
                    WITH je_data AS (
                        SELECT 
                            CASE 
                                WHEN p1.Code LIKE '1%' THEN 'ACTIVO CIRCULANTE'
                                WHEN p1.Code LIKE '2%' THEN 'PASIVO CIRCULANTE'
                                WHEN p1.Code LIKE '3%' THEN 'PATRIMONIO'
                                WHEN p1.Code LIKE '4%' THEN 'INGRESOS'
                                WHEN p1.Code LIKE '52%' THEN 'COSTO DE VENTAS'
                                WHEN p1.Code LIKE '53%' THEN 'GASTOS'
                                ELSE 'OTRA'
                            END AS AccountingType,
                            p1.Code AS PUC_Code,
                            p1.Name AS PUC_Name,
                            CASE 
                                WHEN jl.Debit > 0 THEN jl.Debit 
                                ELSE -jl.Credit
                            END AS SignedAmount
                        FROM JournalLines jl
                        JOIN JournalEntries je ON jl.JournalEntryId = je.Id
                        JOIN PUC_Accounts p1 ON jl.AccountId = p1.Id
                        WHERE je.EntryDate <= @date
                    )
                    SELECT 
                        AccountingType,
                        PUC_Code,
                        PUC_Name,
                        SUM(SignedAmount) AS Balance
                    FROM je_data
                    WHERE AccountingType IN ('ACTIVO CIRCULANTE', 'PASIVO CIRCULANTE', 'PATRIMONIO')
                    GROUP BY AccountingType, PUC_Code, PUC_Name
                    ORDER BY AccountingType, PUC_Code";

                var parameters = new Dictionary<string, object> { ["@date"] = asOfDate };
                return conexion.ejecutarConsultaParametrizada(query, parameters);
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowValidationError($"Error generating Balance Sheet: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Task 32: Generate Income Statement for a period
        /// Shows Revenues, Cost of Sales, and Expenses per Colombian PUC
        /// </summary>
        public static DataTable GenerateIncomeStatement(DateTime startDate, DateTime endDate)
        {
            try
            {
                conexionModel conexion = new conexionModel();

                const string query = @"
                    -- Income Statement for period
                    WITH ie_data AS (
                        SELECT 
                            CASE 
                                WHEN p1.Code LIKE '4%' THEN 'INGRESOS OPERACIONALES'
                                WHEN p1.Code LIKE '52%' THEN 'COSTO DE VENTAS'
                                WHEN p1.Code LIKE '53%' THEN 'GASTOS OPERACIONALES'
                                ELSE 'OTROS'
                            END AS IncomeType,
                            p1.Code AS PUC_Code,
                            p1.Name AS PUC_Name,
                            CASE 
                                WHEN jl.Debit > 0 THEN jl.Debit 
                                ELSE -jl.Credit
                            END AS SignedAmount
                        FROM JournalLines jl
                        JOIN JournalEntries je ON jl.JournalEntryId = je.Id
                        JOIN PUC_Accounts p1 ON jl.AccountId = p1.Id
                        WHERE je.EntryDate >= @startDate 
                          AND je.EntryDate <= @endDate
                          AND p1.Code LIKE '[45][0-9]%'  -- Account types 4 and 5 (income, costs, expenses)
                    )
                    SELECT 
                        IncomeType,
                        PUC_Code,
                        PUC_Name,
                        SUM(SignedAmount) AS Amount
                    FROM ie_data
                    GROUP BY IncomeType, PUC_Code, PUC_Name
                    ORDER BY IncomeType, PUC_Code";

                var parameters = new Dictionary<string, object>
                {
                    ["@startDate"] = startDate,
                    ["@endDate"] = endDate
                };
                return conexion.ejecutarConsultaParametrizada(query, parameters);
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowValidationError($"Error generating Income Statement: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Task 32: Generate detailed Balance Sheet with subtotals
        /// </summary>
        public static string GenerateBalanceSheetHTML(DateTime asOfDate)
        {
            try
            {
                var coCulture = new CultureInfo("es-CO");
                DataTable bs = GenerateBalanceSheet(asOfDate);

                if (bs == null || bs.Rows.Count == 0)
                    return "<p>No data available for Balance Sheet</p>";

                decimal totalAssets = 0, totalLiabilities = 0, totalEquity = 0;

                string html = $@"
<html>
<head>
    <meta charset='UTF-8'>
    <title>Balance Sheet - {asOfDate:yyyy-MM-dd}</title>
    <style>
        body {{ font-family: Arial, sans-serif; padding: 20px; }}
        h1, h2 {{ color: #333; }}
        table {{ width: 100%; border-collapse: collapse; margin: 20px 0; }}
        th {{ background-color: #4a4a4a; color: #fff; padding: 10px; text-align: left; }}
        td {{ padding: 8px; border-bottom: 1px solid #ddd; }}
        .section {{ margin-top: 20px; padding: 10px; background-color: #f5f5f5; font-weight: bold; }}
        .subtotal {{ text-align: right; font-weight: bold; background-color: #e0e0e0; }}
        .amount {{ text-align: right; }}
    </style>
</head>
<body>
<h1>BALANCE GENERAL</h1>
<p>Al: {asOfDate:dd de MMMM de yyyy}</p>
<table>
    <tr>
        <th>Cuenta</th>
        <th>Código PUC</th>
        <th class='amount'>Monto</th>
    </tr>";

                string currentSection = "";
                foreach (DataRow row in bs.Rows)
                {
                    string section = row["AccountingType"].ToString();
                    if (section != currentSection)
                    {
                        html += $@"<tr><td colspan='3' class='section'>{section}</td></tr>";
                        currentSection = section;
                    }

                    decimal amount = Convert.ToDecimal(row["Balance"]);
                    string amountFormatted = amount.ToString("N2", coCulture);

                    if (section.Contains("ACTIVO"))
                        totalAssets += amount;
                    else if (section.Contains("PASIVO"))
                        totalLiabilities += amount;
                    else if (section == "PATRIMONIO")
                        totalEquity += amount;

                    html += $@"
    <tr>
        <td>{row["PUC_Name"]}</td>
        <td>{row["PUC_Code"]}</td>
        <td class='amount'>{amountFormatted}</td>
    </tr>";
                }

                html += $@"
    <tr class='subtotal'>
        <td colspan='2'>TOTAL ASSETS</td>
        <td class='amount'>{totalAssets.ToString("N2", coCulture)}</td>
    </tr>
    <tr class='subtotal'>
        <td colspan='2'>TOTAL LIABILITIES</td>
        <td class='amount'>{totalLiabilities.ToString("N2", coCulture)}</td>
    </tr>
    <tr class='subtotal'>
        <td colspan='2'>TOTAL EQUITY</td>
        <td class='amount'>{totalEquity.ToString("N2", coCulture)}</td>
    </tr>
    <tr class='subtotal' style='border-top: 2px solid #333; font-size: 16px;'>
        <td colspan='2'>TOTAL LIABILITIES + EQUITY</td>
        <td class='amount'>{(totalLiabilities + totalEquity).ToString("N2", coCulture)}</td>
    </tr>
</table>
<p style='color: #666; margin-top: 40px;'>Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}</p>
</body>
</html>";
                return html;
            }
            catch (Exception ex)
            {
                return $"<p>Error: {ex.Message}</p>";
            }
        }

        /// <summary>
        /// Task 32: Generate detailed Income Statement with subtotals
        /// </summary>
        public static string GenerateIncomeStatementHTML(DateTime startDate, DateTime endDate)
        {
            try
            {
                var coCulture = new CultureInfo("es-CO");
                DataTable is_data = GenerateIncomeStatement(startDate, endDate);

                if (is_data == null || is_data.Rows.Count == 0)
                    return "<p>No data available for Income Statement</p>";

                decimal totalRevenue = 0, totalCogs = 0, totalExpenses = 0;

                string html = $@"
<html>
<head>
    <meta charset='UTF-8'>
    <title>Income Statement - {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}</title>
    <style>
        body {{ font-family: Arial, sans-serif; padding: 20px; }}
        h1, h2 {{ color: #333; }}
        table {{ width: 100%; border-collapse: collapse; margin: 20px 0; }}
        th {{ background-color: #4a4a4a; color: #fff; padding: 10px; text-align: left; }}
        td {{ padding: 8px; border-bottom: 1px solid #ddd; }}
        .section {{ margin-top: 20px; padding: 10px; background-color: #f5f5f5; font-weight: bold; }}
        .subtotal {{ text-align: right; font-weight: bold; background-color: #e0e0e0; }}
        .amount {{ text-align: right; }}
    </style>
</head>
<body>
<h1>ESTADO DE RESULTADOS</h1>
<p>Período: {startDate:dd/MM/yyyy} a {endDate:dd/MM/yyyy}</p>
<table>
    <tr>
        <th>Cuenta</th>
        <th>Código PUC</th>
        <th class='amount'>Monto</th>
    </tr>";

                string currentSection = "";
                foreach (DataRow row in is_data.Rows)
                {
                    string section = row["IncomeType"].ToString();
                    if (section != currentSection)
                    {
                        html += $@"<tr><td colspan='3' class='section'>{section}</td></tr>";
                        currentSection = section;
                    }

                    decimal amount = Convert.ToDecimal(row["Amount"]);
                    string amountFormatted = amount.ToString("N2", coCulture);

                    if (section.Contains("INGRESOS"))
                        totalRevenue += amount;
                    else if (section.Contains("COSTO"))
                        totalCogs += amount;
                    else if (section.Contains("GASTOS"))
                        totalExpenses += amount;

                    html += $@"
    <tr>
        <td>{row["PUC_Name"]}</td>
        <td>{row["PUC_Code"]}</td>
        <td class='amount'>{amountFormatted}</td>
    </tr>";
                }

                decimal netIncome = totalRevenue - totalCogs - totalExpenses;

                html += $@"
    <tr class='subtotal'>
        <td colspan='2'>TOTAL REVENUE</td>
        <td class='amount'>{totalRevenue.ToString("N2", coCulture)}</td>
    </tr>
    <tr class='subtotal'>
        <td colspan='2'>COST OF GOODS SOLD</td>
        <td class='amount'>({totalCogs.ToString("N2", coCulture)})</td>
    </tr>
    <tr class='subtotal'>
        <td colspan='2'>GROSS PROFIT</td>
        <td class='amount'>{(totalRevenue - totalCogs).ToString("N2", coCulture)}</td>
    </tr>
    <tr class='subtotal'>
        <td colspan='2'>OPERATING EXPENSES</td>
        <td class='amount'>({totalExpenses.ToString("N2", coCulture)})</td>
    </tr>
    <tr class='subtotal' style='border-top: 2px solid #333; font-size: 16px;'>
        <td colspan='2'>NET INCOME</td>
        <td class='amount' style='color: {(netIncome >= 0 ? "green" : "red")};'>{netIncome.ToString("N2", coCulture)}</td>
    </tr>
</table>
<p style='color: #666; margin-top: 40px;'>Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}</p>
</body>
</html>";
                return html;
            }
            catch (Exception ex)
            {
                return $"<p>Error: {ex.Message}</p>";
            }
        }
    }
}

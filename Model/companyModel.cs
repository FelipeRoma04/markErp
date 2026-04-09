using System;
using System.Data;
using Proyecto.Controler;

namespace Proyecto.Model
{
    /// <summary>
    /// FASE 4 - Task 37: Company Model
    /// Handles database operations for company configuration
    /// Singleton pattern - only one company record (Id = 1)
    /// </summary>
    public class companyModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Nit { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string LogoPath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Get company settings (always returns record with Id=1)
        /// </summary>
        public companyModel GetCompanySettings()
        {
            try
            {
                conexionModel conexion = new conexionModel();
                const string query = @"
                    SELECT TOP 1 
                        Id, CompanyName, Nit, Address, City, Phone, Email, 
                        LogoPath, CreatedDate, UpdatedDate
                    FROM Company
                    WHERE Id = 1";

                DataTable dt = conexion.ejecutarConsulta(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new companyModel
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        CompanyName = row["CompanyName"].ToString(),
                        Nit = row["Nit"].ToString(),
                        Address = row["Address"].ToString(),
                        City = row["City"].ToString(),
                        Phone = row["Phone"].ToString(),
                        Email = row["Email"].ToString(),
                        LogoPath = row["LogoPath"].ToString(),
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                        UpdatedDate = Convert.ToDateTime(row["UpdatedDate"])
                    };
                }
                else
                {
                    // Return default if not found
                    return new companyModel
                    {
                        Id = 1,
                        CompanyName = "Mi Empresa",
                        Nit = "0000000000"
                    };
                }
            }
            catch (Exception)
            {
                // Log the error - can't use AuditLogger.LogError since it doesn't exist
                throw;
            }
        }

        /// <summary>
        /// Save or update company settings
        /// </summary>
        public bool SaveCompanySettings()
        {
            try
            {
                conexionModel conexion = new conexionModel();

                // Check if company record exists
                const string checkQuery = "SELECT COUNT(*) as cnt FROM Company WHERE Id = 1";
                DataTable checkDt = conexion.ejecutarConsulta(checkQuery);
                bool exists = checkDt != null && Convert.ToInt32(checkDt.Rows[0]["cnt"]) > 0;

                string query;
                if (exists)
                {
                    query = @"
                        UPDATE Company 
                        SET CompanyName = @name, 
                            Nit = @nit, 
                            Address = @address,
                            City = @city, 
                            Phone = @phone, 
                            Email = @email, 
                            LogoPath = @logoPath,
                            UpdatedDate = GETDATE()
                        WHERE Id = 1";
                }
                else
                {
                    query = @"
                        INSERT INTO Company (Id, CompanyName, Nit, Address, City, Phone, Email, LogoPath, CreatedDate, UpdatedDate)
                        VALUES (1, @name, @nit, @address, @city, @phone, @email, @logoPath, GETDATE(), GETDATE())";
                }

                var parameters = new System.Collections.Generic.Dictionary<string, object>
                {
                    ["@name"] = this.CompanyName ?? "",
                    ["@nit"] = this.Nit ?? "",
                    ["@address"] = this.Address ?? "",
                    ["@city"] = this.City ?? "",
                    ["@phone"] = this.Phone ?? "",
                    ["@email"] = this.Email ?? "",
                    ["@logoPath"] = this.LogoPath ?? ""
                };

                conexion.ejecutarComandoParametrizado(query, parameters);

                AuditLogger.LogUpdate("Company", "1", $"Company settings updated: {this.CompanyName}");

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get company name for UI display (used in forms, reports, invoices)
        /// </summary>
        public static string GetCompanyName()
        {
            try
            {
                companyModel model = new companyModel();
                var company = model.GetCompanySettings();
                return !string.IsNullOrEmpty(company.CompanyName) ? company.CompanyName : "Mi Empresa";
            }
            catch
            {
                return "Mi Empresa";
            }
        }

        /// <summary>
        /// Get company NIT for invoice/payroll headers
        /// </summary>
        public static string GetCompanyNit()
        {
            try
            {
                companyModel model = new companyModel();
                var company = model.GetCompanySettings();
                return !string.IsNullOrEmpty(company.Nit) ? company.Nit : "0000000000";
            }
            catch
            {
                return "0000000000";
            }
        }

        /// <summary>
        /// Get company logo path
        /// </summary>
        public static string GetLogoPath()
        {
            try
            {
                companyModel model = new companyModel();
                var company = model.GetCompanySettings();
                return company.LogoPath ?? "";
            }
            catch
            {
                return "";
            }
        }
    }
}

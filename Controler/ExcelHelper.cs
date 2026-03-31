using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Proyecto.Controler
{
    /// <summary>
    /// Excel export helper (simplified - ClosedXML not available)
    /// Exports to CSV format instead
    /// </summary>
    public static class ExcelHelper
    {
        /// <summary>
        /// Export DataTable to CSV file
        /// </summary>
        public static bool ExportToExcel(DataTable data, string filePath, string sheetName = "Data")
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                
                // Add headers
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    sb.Append("\"" + data.Columns[i].ColumnName + "\"");
                    if (i < data.Columns.Count - 1)
                        sb.Append(",");
                }
                sb.AppendLine();

                // Add data
                for (int row = 0; row < data.Rows.Count; row++)
                {
                    for (int col = 0; col < data.Columns.Count; col++)
                    {
                        var value = data.Rows[row][col]?.ToString() ?? "";
                        sb.Append("\"" + value.Replace("\"", "\"\"") + "\"");
                        if (col < data.Columns.Count - 1)
                            sb.Append(",");
                    }
                    sb.AppendLine();
                }

                // Add footer with date
                sb.AppendLine();
                sb.Append("\"Generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "\"");

                // Change extension to .csv if .xlsx was requested
                string outputPath = filePath.EndsWith(".xlsx") 
                    ? filePath.Replace(".xlsx", ".csv") 
                    : filePath;

                File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Export multiple DataTables to CSV files (one per table)
        /// </summary>
        public static bool ExportMultipleTablesToExcel(Dictionary<string, DataTable> tables, string filePath)
        {
            try
            {
                string directory = Path.GetDirectoryName(filePath);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);

                foreach (var kvp in tables)
                {
                    string tableFilePath = Path.Combine(directory, fileNameWithoutExt + "_" + kvp.Key + ".csv");
                    if (!ExportToExcel(kvp.Value, tableFilePath, kvp.Key))
                        return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

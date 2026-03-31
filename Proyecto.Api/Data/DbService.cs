using Microsoft.Data.SqlClient;
using System.Data;

namespace Proyecto.Api.Data
{
    /// <summary>
    /// Thin ADO.NET wrapper shared across all API controllers.
    /// </summary>
    public class DbService
    {
        private readonly string _connectionString;

        public DbService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<DataTable> QueryAsync(string sql, Dictionary<string, object>? parameters = null)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            if (parameters != null)
                foreach (var p in parameters)
                    cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);

            await conn.OpenAsync();
            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public async Task<bool> ExecuteAsync(string sql, Dictionary<string, object>? parameters = null)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(sql, conn);

                if (parameters != null)
                    foreach (var p in parameters)
                        cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                return true;
            }
            catch { return false; }
        }

        /// <summary>Converts a DataTable to a list of dictionaries (JSON-serializable).</summary>
        public static List<Dictionary<string, object?>> ToList(DataTable dt)
        {
            var list = new List<Dictionary<string, object?>>();
            foreach (DataRow row in dt.Rows)
            {
                var dict = new Dictionary<string, object?>();
                foreach (DataColumn col in dt.Columns)
                    dict[col.ColumnName] = row[col] == DBNull.Value ? null : row[col];
                list.Add(dict);
            }
            return list;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Proyecto.Model;

namespace Proyecto.Controler
{
    /// <summary>
    /// FASE 3 - Task 36: Dashboard Charts
    /// Replaces text KPIs with visual charts
    /// Charts: Monthly sales bar chart, Inventory by category pie chart
    /// </summary>
    public static class DashboardCharts
    {
        /// <summary>
        /// Task 36: Create monthly sales bar chart
        /// </summary>
        public static Bitmap GenerateMonthlySalesChart(DateTime? endDate = null)
        {
            try
            {
                if (endDate == null)
                    endDate = DateTime.Now;

                conexionModel conexion = new conexionModel();

                // Query sales by month for last 12 months
                string query = @"
                    SELECT 
                        MONTH(IssueDate) AS Mes,
                        YEAR(IssueDate) AS Año,
                        SUM(Total) AS Ventas
                    FROM Invoices
                    WHERE IssueDate >= DATEADD(MONTH, -12, @date)
                      AND IssueDate <= @date
                    GROUP BY YEAR(IssueDate), MONTH(IssueDate)
                    ORDER BY Año, Mes";

                var parameters = new Dictionary<string, object> { ["@date"] = endDate };
                DataTable salesData = conexion.ejecutarConsultaParametrizada(query, parameters);

                if (salesData == null || salesData.Rows.Count == 0)
                    return CreateEmptyChart("No sales data available");

                return DrawBarChart(salesData, "VENTAS POR MES (ÚLTIMOS 12 MESES)", "Mes", "Ventas");
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowValidationError($"Error generating chart: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Task 36: Create inventory by category pie chart
        /// </summary>
        public static Bitmap GenerateInventoryByCategory()
        {
            try
            {
                conexionModel conexion = new conexionModel();

                // Query inventory value by category
                const string query = @"
                    SELECT 
                        Category,
                        COUNT(*) AS Productos,
                        SUM(Stock * Price) AS ValorInventario
                    FROM Products
                    WHERE Stock > 0
                    GROUP BY Category
                    ORDER BY ValorInventario DESC";

                DataTable categoryData = conexion.ejecutarConsulta(query);

                if (categoryData == null || categoryData.Rows.Count == 0)
                    return CreateEmptyChart("No inventory data available");

                return DrawPieChart(categoryData, "INVENTARIO POR CATEGORÍA", "ValorInventario", "Category");
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowValidationError($"Error generating chart: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Task 36: Draw bar chart from DataTable
        /// </summary>
        private static Bitmap DrawBarChart(DataTable data, string title, string xAxisLabel, string yAxisLabel)
        {
            int width = 800, height = 400;
            Bitmap bitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitmap);

            // Background
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, width, height);
            g.DrawRectangle(new Pen(Color.Black, 2), 0, 0, width - 1, height - 1);

            // Title
            Font titleFont = new Font("Arial", 14, FontStyle.Bold);
            g.DrawString(title, titleFont, Brushes.Black, width / 2 - 100, 10);

            // Chart area
            int chartLeft = 60, chartTop = 50, chartWidth = 700, chartHeight = 300;
            g.DrawRectangle(new Pen(Color.Gray), chartLeft, chartTop, chartWidth, chartHeight);

            if (data.Rows.Count == 0)
            {
                g.DrawString("No data", new Font("Arial", 12), Brushes.Red, chartLeft + 10, chartTop + 10);
                g.Dispose();
                return bitmap;
            }

            // Get max value for scaling
            decimal maxValue = 0;
            foreach (DataRow row in data.Rows)
            {
                decimal value = Convert.ToDecimal(row[yAxisLabel] ?? 0);
                if (value > maxValue) maxValue = value;
            }

            if (maxValue == 0) maxValue = 1;

            // Draw bars
            float barWidth = chartWidth / (float)data.Rows.Count;
            Color[] colors = { Color.FromArgb(52, 152, 219), Color.FromArgb(46, 204, 113), 
                             Color.FromArgb(241, 196, 15), Color.FromArgb(230, 126, 34) };

            for (int i = 0; i < data.Rows.Count; i++)
            {
                decimal value = Convert.ToDecimal(data.Rows[i][yAxisLabel] ?? 0);
                float barHeight = (float)(value / maxValue) * chartHeight;

                float barX = chartLeft + (i * barWidth) + 5;
                float barY = chartTop + chartHeight - barHeight;
                Color barColor = colors[i % colors.Length];

                g.FillRectangle(new SolidBrush(barColor), barX, barY, barWidth - 10, barHeight);
                g.DrawRectangle(new Pen(Color.Black), barX, barY, barWidth - 10, barHeight);

                // X-axis label
                string label = data.Rows[i][xAxisLabel]?.ToString() ?? "";
                g.DrawString(label, new Font("Arial", 9), Brushes.Black, barX, chartTop + chartHeight + 10);
            }

            // Y-axis label
            g.DrawString(yAxisLabel, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 10, chartTop);

            g.Dispose();
            return bitmap;
        }

        /// <summary>
        /// Task 36: Draw pie chart from DataTable
        /// </summary>
        private static Bitmap DrawPieChart(DataTable data, string title, string valueColumn, string labelColumn)
        {
            int width = 800, height = 400;
            Bitmap bitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitmap);

            // Background
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, width, height);
            g.DrawRectangle(new Pen(Color.Black, 2), 0, 0, width - 1, height - 1);

            // Title
            Font titleFont = new Font("Arial", 14, FontStyle.Bold);
            g.DrawString(title, titleFont, Brushes.Black, width / 2 - 120, 10);

            if (data.Rows.Count == 0)
            {
                g.DrawString("No data", new Font("Arial", 12), Brushes.Red, width / 2 - 30, height / 2);
                g.Dispose();
                return bitmap;
            }

            // Calculate total
            decimal total = 0;
            foreach (DataRow row in data.Rows)
            {
                total += Convert.ToDecimal(row[valueColumn] ?? 0);
            }

            if (total == 0) total = 1;

            // Draw pie
            int pieX = 50, pieY = 60, pieDiameter = 250;
            float startAngle = 0;
            Color[] colors = { Color.FromArgb(52, 152, 219), Color.FromArgb(46, 204, 113), 
                             Color.FromArgb(241, 196, 15), Color.FromArgb(230, 126, 34),
                             Color.FromArgb(155, 89, 182), Color.FromArgb(52, 73, 94) };

            int colorIndex = 0;
            int legendX = pieX + pieDiameter + 50;
            int legendY = 80;

            foreach (DataRow row in data.Rows)
            {
                decimal value = Convert.ToDecimal(row[valueColumn] ?? 0);
                float sweepAngle = (float)(value / total * 360);

                Color pieColor = colors[colorIndex % colors.Length];
                g.FillPie(new SolidBrush(pieColor), pieX, pieY, pieDiameter, pieDiameter, startAngle, sweepAngle);
                g.DrawPie(new Pen(Color.Black, 1), pieX, pieY, pieDiameter, pieDiameter, startAngle, sweepAngle);

                // Legend
                g.FillRectangle(new SolidBrush(pieColor), legendX, legendY, 15, 15);
                g.DrawRectangle(new Pen(Color.Black), legendX, legendY, 15, 15);

                string label = $"{row[labelColumn]} ({(value / total * 100):F0}%)";
                g.DrawString(label, new Font("Arial", 10), Brushes.Black, legendX + 20, legendY);

                legendY += 25;
                startAngle += sweepAngle;
                colorIndex++;
            }

            g.Dispose();
            return bitmap;
        }

        /// <summary>
        /// Create placeholder for empty chart
        /// </summary>
        private static Bitmap CreateEmptyChart(string message)
        {
            Bitmap bitmap = new Bitmap(800, 400);
            Graphics g = Graphics.FromImage(bitmap);
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, 800, 400);
            g.DrawRectangle(new Pen(Color.Black, 2), 0, 0, 799, 399);
            g.DrawString(message, new Font("Arial", 14), Brushes.Gray, 250, 190);
            g.Dispose();
            return bitmap;
        }
    }
}

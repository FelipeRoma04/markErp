using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Proyecto.Controler;
using Proyecto.Model;

namespace Proyecto.View
{
    public partial class frmReports : Form
    {
        public frmReports()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            cmbReportType.Items.Add("Empleados");
            cmbReportType.Items.Add("Departamentos");
            cmbReportType.Items.Add("Clientes");
            cmbReportType.Items.Add("Productos");
            cmbReportType.Items.Add("Facturas");
            cmbReportType.Items.Add("Nóminas");
            cmbReportType.Items.Add("Activos");
            // Phase 3 Reports
            cmbReportType.Items.Add("Reporte Ventas por Período");
            cmbReportType.Items.Add("Reporte Nómina del Período");
            cmbReportType.Items.Add("Inventario Crítico (PDF)");
            cmbReportType.Items.Add("Estado Financiero - Balance");
            cmbReportType.Items.Add("Estado Financiero - Resultados");

            cmbExportFormat.Items.Add("Excel (.xlsx)");
            cmbExportFormat.Items.Add("PDF");
            cmbExportFormat.SelectedIndex = 0;
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                string reportType = cmbReportType.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(reportType))
                {
                    ValidationHelper.ShowValidationError("Selecciona un tipo de reporte.");
                    return;
                }

                // Phase 3: Period-based reports need special handling
                if (reportType == "Reporte Ventas por Período")
                {
                    HandleSalesReport();
                    return;
                }
                else if (reportType == "Reporte Nómina del Período")
                {
                    HandlePayrollReport();
                    return;
                }
                else if (reportType == "Inventario Crítico (PDF)")
                {
                    HandleCriticalStockReport();
                    return;
                }
                else if (reportType == "Estado Financiero - Balance")
                {
                    HandleBalanceSheetReport();
                    return;
                }
                else if (reportType == "Estado Financiero - Resultados")
                {
                    HandleIncomeStatementReport();
                    return;
                }

                // Standard reports
                DataTable reportData = GetReportData(reportType);
                if (reportData == null || reportData.Rows.Count == 0)
                {
                    ValidationHelper.ShowValidationError("No hay datos para el reporte seleccionado.");
                    return;
                }

                dgvReportPreview.DataSource = reportData;
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error generando reporte: " + ex.Message);
            }
        }

        // Task 33: Sales report handler
        private void HandleSalesReport()
        {
            Form dateDialog = CreateDateRangeDialog("Reporte de Ventas");
            if (dateDialog.ShowDialog() == DialogResult.OK)
            {
                DateTime startDate = (DateTime)dateDialog.Tag == new object[] 
                    ? DateTime.Now.AddMonths(-1) 
                    : ((DateTime[])dateDialog.Tag)[0];
                DateTime endDate = ((DateTime[])dateDialog.Tag)[1];

                SalesReportExporter.GenerateSalesReportExcel(startDate, endDate);
            }
        }

        // Task 34: Payroll report handler
        private void HandlePayrollReport()
        {
            Form dateDialog = CreateDateRangeDialog("Reporte de Nómina");
            if (dateDialog.ShowDialog() == DialogResult.OK)
            {
                DateTime startDate = ((DateTime[])dateDialog.Tag)[0];
                DateTime endDate = ((DateTime[])dateDialog.Tag)[1];

                PayrollReportExporter.GeneratePayrollReportExcel(startDate, endDate);
            }
        }

        // Task 35: Critical stock report handler
        private void HandleCriticalStockReport()
        {
            CriticalStockReportPDF.GenerateCriticalStockReportPDF();
        }

        // Task 32: Balance sheet report handler
        private void HandleBalanceSheetReport()
        {
            var dialog = new Form();
            dialog.Text = "Seleccionar Fecha";
            dialog.Size = new System.Drawing.Size(300, 150);

            DateTimePicker dtp = new DateTimePicker { Value = DateTime.Now };
            dtp.Location = new System.Drawing.Point(10, 20);
            dtp.Width = 260;

            Button btnOk = new Button { Text = "Generar", DialogResult = DialogResult.OK };
            btnOk.Location = new System.Drawing.Point(100, 70);

            dialog.Controls.Add(new Label { Text = "Al date:", Location = new System.Drawing.Point(10, 0) });
            dialog.Controls.Add(dtp);
            dialog.Controls.Add(btnOk);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string html = FinancialReportGenerator.GenerateBalanceSheetHTML(dtp.Value);
                SaveAndOpenHTML(html, "Balance_Sheet");
            }
        }

        // Task 32: Income statement report handler
        private void HandleIncomeStatementReport()
        {
            Form dateDialog = CreateDateRangeDialog("Estado de Resultados");
            if (dateDialog.ShowDialog() == DialogResult.OK)
            {
                DateTime startDate = ((DateTime[])dateDialog.Tag)[0];
                DateTime endDate = ((DateTime[])dateDialog.Tag)[1];

                string html = FinancialReportGenerator.GenerateIncomeStatementHTML(startDate, endDate);
                SaveAndOpenHTML(html, "Income_Statement");
            }
        }

        private Form CreateDateRangeDialog(string title)
        {
            var dialog = new Form();
            dialog.Text = title;
            dialog.Size = new System.Drawing.Size(350, 200);
            dialog.StartPosition = FormStartPosition.CenterParent;

            DateTimePicker dtpStart = new DateTimePicker { Value = DateTime.Now.AddMonths(-1) };
            dtpStart.Location = new System.Drawing.Point(10, 30);

            DateTimePicker dtpEnd = new DateTimePicker { Value = DateTime.Now };
            dtpEnd.Location = new System.Drawing.Point(10, 80);

            Button btnOk = new Button { Text = "Aceptar", DialogResult = DialogResult.OK };
            btnOk.Location = new System.Drawing.Point(150, 130);

            Button btnCancel = new Button { Text = "Cancelar", DialogResult = DialogResult.Cancel };
            btnCancel.Location = new System.Drawing.Point(240, 130);

            dialog.Controls.Add(new Label { Text = "Desde:", Location = new System.Drawing.Point(10, 10) });
            dialog.Controls.Add(dtpStart);
            dialog.Controls.Add(new Label { Text = "Hasta:", Location = new System.Drawing.Point(10, 60) });
            dialog.Controls.Add(dtpEnd);
            dialog.Controls.Add(btnOk);
            dialog.Controls.Add(btnCancel);

            dialog.Tag = new DateTime[] { dtpStart.Value, dtpEnd.Value };

            dtpStart.ValueChanged += (s, e) => ((DateTime[])dialog.Tag)[0] = dtpStart.Value;
            dtpEnd.ValueChanged += (s, e) => ((DateTime[])dialog.Tag)[1] = dtpEnd.Value;

            return dialog;
        }

        private void SaveAndOpenHTML(string html, string filename)
        {
            try
            {
                string path = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"{filename}_{DateTime.Now:yyyyMMdd_HHmmss}.html");

                System.IO.File.WriteAllText(path, html);
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(path) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError($"Error: {ex.Message}");
            }
        }

        private DataTable GetReportData(string reportType)
        {
            try
            {
                conexionModel conn = new conexionModel();

                return reportType switch
                {
                    "Empleados" => conn.ejecutarConsulta("SELECT * FROM Empleados ORDER BY Id DESC"),
                    "Departamentos" => conn.ejecutarConsulta("SELECT * FROM Departamentos ORDER BY Id DESC"),
                    "Clientes" => conn.ejecutarConsulta("SELECT * FROM Clients ORDER BY Id DESC"),
                    "Productos" => conn.ejecutarConsulta("SELECT * FROM Products ORDER BY Id DESC"),
                    "Facturas" => conn.ejecutarConsulta("SELECT * FROM Invoices ORDER BY Id DESC"),
                    "Nóminas" => conn.ejecutarConsulta("SELECT * FROM Payroll_Log ORDER BY Id DESC"),
                    "Activos" => conn.ejecutarConsulta("SELECT * FROM Assets ORDER BY Id DESC"),
                    _ => null
                };
            }
            catch
            {
                return null;
            }
        }

        private void btnExportReport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data = dgvReportPreview.DataSource as DataTable;
                if (data == null || data.Rows.Count == 0)
                {
                    ValidationHelper.ShowValidationError("Genera un reporte primero.");
                    return;
                }

                string format = cmbExportFormat.SelectedItem?.ToString();
                if (format == "Excel (.xlsx)")
                {
                    ExportToExcel(data);
                }
                else if (format == "PDF")
                {
                    ExportToPdf(data);
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error exportando: " + ex.Message);
            }
        }

        private void ExportToExcel(DataTable data)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                FileName = $"Report_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (ExcelHelper.ExportToExcel(data, sfd.FileName, "Reporte"))
                {
                    ValidationHelper.ShowSuccess($"Reporte exportado a: {sfd.FileName}");
                }
                else
                {
                    ValidationHelper.ShowError("Error al exportar a Excel.");
                }
            }
        }

        private void ExportToPdf(DataTable data)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                FileName = $"Report_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // TODO: Implement PDF table export using iTextSharp
                ValidationHelper.ShowSuccess("Funcionalidad de PDF en desarrollo.");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dgvReportPreview.DataSource = null;
            cmbReportType.SelectedIndex = -1;
        }
    }
}

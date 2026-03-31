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

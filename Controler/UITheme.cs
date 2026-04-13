using System;
using System.Drawing;
using System.Windows.Forms;

namespace Proyecto.Controler
{
    public static class UITheme
    {
        // Colors - Sierra Alta Modern Palette
        public static readonly Color PrimaryColor = Color.FromArgb(44, 62, 80);      // Dark Navy
        public static readonly Color SecondaryColor = Color.FromArgb(52, 73, 94);    // Lighter Navy
        public static readonly Color AccentColor = Color.FromArgb(230, 126, 34);     // Sierra Orange
        public static readonly Color SuccessColor = Color.FromArgb(39, 174, 96);     // Emerald
        public static readonly Color DangerColor = Color.FromArgb(192, 57, 43);      // Alizarin Red
        public static readonly Color WarningColor = Color.FromArgb(243, 156, 18);    // Orange/Yellow
        public static readonly Color BgColor = Color.FromArgb(236, 240, 241);        // Cloud Gray
        public static readonly Color SurfaceColor = Color.White;
        public static readonly Color TextPrimary = Color.FromArgb(44, 62, 80);
        public static readonly Color TextOnPrimary = Color.White;
        public static readonly Color TextSecondary = Color.FromArgb(127, 140, 141);

        // Fonts
        public static readonly Font FontHeader = new Font("Segoe UI", 14F, FontStyle.Bold);
        public static readonly Font FontSubHeader = new Font("Segoe UI", 11F, FontStyle.Bold);
        public static readonly Font FontBodyBold = new Font("Segoe UI", 10F, FontStyle.Bold);
        public static readonly Font FontBody = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static readonly Font FontSmall = new Font("Segoe UI", 8.5F, FontStyle.Regular);

        public static void ApplyModernStyle(Button btn, bool isPrimary = false, bool isDanger = false)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = isPrimary || isDanger ? 0 : 1;
            btn.Font = FontBodyBold;
            btn.Cursor = Cursors.Hand;
            btn.Height = 35;

            if (isDanger)
            {
                btn.BackColor = DangerColor;
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderColor = DangerColor;
            }
            else if (isPrimary)
            {
                btn.BackColor = AccentColor;
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderColor = AccentColor;
            }
            else
            {
                btn.BackColor = SurfaceColor;
                btn.ForeColor = TextPrimary;
                btn.FlatAppearance.BorderColor = SecondaryColor;
            }
        }

        public static void StyleSecondaryButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = Color.White;
            btn.ForeColor = TextPrimary;
            btn.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199);
            btn.FlatAppearance.BorderSize = 1;
            btn.Font = FontBody;
            btn.Cursor = Cursors.Hand;
        }

        public static void StyleLabel(Label lbl, bool isHeader = false)
        {
            lbl.Font = isHeader ? FontHeader : FontBody;
            lbl.ForeColor = TextPrimary;
        }

        public static void StyleTextBox(TextBox txt)
        {
            txt.Font = FontBody;
            txt.BorderStyle = BorderStyle.FixedSingle;
        }
        
        public static void StyleDataGrid(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = PrimaryColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = FontBodyBold;
            dgv.DefaultCellStyle.Font = FontBody;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.RowHeadersVisible = false;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
        }
    }
}

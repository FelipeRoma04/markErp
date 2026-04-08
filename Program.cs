using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Proyecto.View;

namespace Proyecto
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) => HandleCrash(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is Exception ex) HandleCrash(ex);
            };

            // Show the login form
            frmLogin login = new frmLogin();
            if (login.ShowDialog() == DialogResult.OK)
            {
                // Unlocks Dashboard if authentication was OK
                try
                {
                    Application.Run(new principal());
                }
                catch (Exception ex)
                {
                    HandleCrash(ex);
                }
            }
            else
            {
                Application.Exit();
            }
        }

        private static void HandleCrash(Exception ex)
        {
            try
            {
                var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cliente-crash.log");
                File.AppendAllText(logPath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {ex}\n\n");
            }
            catch { /* ignore logging failures */ }

            // Show the exception details in the dialog to make debugging easier during development
            try
            {
                string message = "Ocurrió un error y la aplicación debe cerrarse.\n" +
                                 "Revisa el archivo cliente-crash.log junto al ejecutable.\n\n" +
                                 "Detalles:\n" + ex.ToString();

                MessageBox.Show(
                    message,
                    "Error inesperado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch
            {
                // If showing the detailed dialog fails for any reason, fall back to the simple message.
                try
                {
                    MessageBox.Show(
                        "Ocurrió un error y la aplicación debe cerrarse.\nRevisa el archivo cliente-crash.log junto al ejecutable.",
                        "Error inesperado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                catch { }
            }

            Environment.Exit(1);
        }
    }
}

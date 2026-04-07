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

            MessageBox.Show(
                "Ocurrió un error y la aplicación debe cerrarse.\n" +
                "Revisa el archivo cliente-crash.log junto al ejecutable y comparte el contenido.",
                "Error inesperado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            Environment.Exit(1);
        }
    }
}

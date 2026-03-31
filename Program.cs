using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            
            // Show the login form
            frmLogin login = new frmLogin();
            if (login.ShowDialog() == DialogResult.OK)
            {
                // Unlocks Dashboard if authentication was OK
                Application.Run(new principal());
            }
            else
            {
                Application.Exit();
            }
        }
    }
}

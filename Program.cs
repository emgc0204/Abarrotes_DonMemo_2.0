using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abarrotes_DonMemo_2._0
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
            Login log = new Login();
            if (log.ShowDialog() == DialogResult.OK )
            {
                Principal principalform = new Principal(log.usuario_actual);
                Application.Run(principalform);
            }
        }
    }
}
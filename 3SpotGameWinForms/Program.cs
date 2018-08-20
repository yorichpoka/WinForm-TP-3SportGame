using _3SpotGameWinForms.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3SpotGameWinForms
{
    static class Program
    {
        public static AppForm appForm { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // -- Create the form -- //
            appForm = new AppForm();

            // -- Lauch the form -- //
            Application.Run(appForm);
        }
    }
}

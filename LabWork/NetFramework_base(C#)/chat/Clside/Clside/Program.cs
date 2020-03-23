using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Clside
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Fclient fc = new Fclient();
        }
    }
}

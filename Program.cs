using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNCJobQueueManager
{
    /// <summary>
    /// The main entry point for the CNC Job Queue Manager application.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Enables visual styles for the application
            Application.EnableVisualStyles();

            // Sets the default text rendering to be compatible with the current operating system
            Application.SetCompatibleTextRenderingDefault(false);

            // Starts the application with the main form
            Application.Run(new MainForm());
        }
    }
}

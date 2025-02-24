using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNCJobQueueManager
{
    /// <summary>
    /// The purpose of this application is to manage a queue of jobs for a CNC machine,
    /// process them asynchronously, and update the status of each job as it is processed. 
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

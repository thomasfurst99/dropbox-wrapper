using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace DropboxWrapper
{
    static class Program
    {
        /// <summary>
        /// Mutex used to create only single instance app.
        /// </summary>
        static Mutex mutex = new Mutex(true, Assembly.GetExecutingAssembly().GetType().GUID.ToString());

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Only one instance of app at time.
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Context());
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("Only one instance at time!", "Error");
                Environment.Exit(1);
            }
        }
    }
}

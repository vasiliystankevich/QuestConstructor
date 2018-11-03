using System;
using System.Reflection;
using System.Windows.Forms;
using Quest.Core.UI;

namespace Quest.Constructor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ExceptionHandler.Init();
            IconForAllForm.SetIcon(Controls.Properties.Resources.question1);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}

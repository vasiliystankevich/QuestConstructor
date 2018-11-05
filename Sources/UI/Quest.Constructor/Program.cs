using System;
using System.Windows.Forms;
using Quest.Core.UI;

namespace Quest.Constructor
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ExceptionHandler.Init();
            IconForAllForm.SetIcon(Controls.Properties.Resources.question1);
            Core.UI.Localizable.SetCulture("ru-RU");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}

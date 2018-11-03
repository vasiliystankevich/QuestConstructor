using System;
using System.Windows.Forms;
using Quest.Core.UI;

namespace Quest.Interview
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ExceptionHandler.Init();
            IconForAllForm.SetIcon(Controls.Properties.Resources.question1);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Controls.QuestInterview.MainForm());
        }
    }
}

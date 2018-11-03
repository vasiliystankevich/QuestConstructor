using System;
using System.Reflection;
using System.Windows.Forms;
using QuestCore.UI;

namespace QuestInterviewNS
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

            //иконка для всех форм
            typeof(Form).GetField("defaultIcon", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, Quest.Controls.Properties.Resources.question1);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Quest.Controls.QuestInterview.MainForm());
        }
    }
}

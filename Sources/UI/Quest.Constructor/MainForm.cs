using System;
using System.Windows.Forms;
using Quest.Controls.Presenters;

namespace Quest.Constructor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Presenter = new QuestConstructor(pnMain, btSave);
            Presenter.Build();
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            Presenter.Open();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            Presenter.Save();
        }

        private void btAddQuest_Click(object sender, EventArgs e)
        {
            Presenter.AddQuest();
        }

        private void btRun_Click(object sender, EventArgs e)
        {
            Presenter.Run(this);
        }

        private void btExportCSV_Click(object sender, EventArgs e)
        {
            Presenter.ExportCsv(this);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Presenter.AskAboutSaveCurrentQuestionnaire();
        }

        IQuestConstructor Presenter { get; }

    }
}

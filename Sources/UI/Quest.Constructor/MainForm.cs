using System;
using System.Windows.Forms;
using Quest.Controls.Presenter;

namespace Quest.Constructor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            presenter = new QuestConstructor(pnMain, btSave);
            presenter.Build();
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            presenter.btOpen_Click();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            presenter.btSave_Click();
        }

        private void btAddQuest_Click(object sender, EventArgs e)
        {
            presenter.btAddQuest_Click();
        }

        private void btRun_Click(object sender, EventArgs e)
        {
            presenter.btRun_Click(this);
        }

        private void btExportCSV_Click(object sender, EventArgs e)
        {
            presenter.btExportCSV_Click(this);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            presenter.MainForm_FormClosing();
        }

        IQuestConstructor presenter { get; set; }

    }
}

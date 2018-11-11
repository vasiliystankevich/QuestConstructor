using System;
using System.Windows.Forms;
using Quest.Core.Model;

namespace Quest.Controls.QuestInterview
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Presenter = new Presenter.QuestInterview(pnAnswers);
        }

        public MainForm(Questionnaire questionnaire) : this()
        {
            Presenter = new Presenter.QuestInterview(pnAnswers, questionnaire);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var result=Presenter.OnLoad(e, this);
            if (result!=DialogResult.OK) Close();
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            Presenter.btNext_Click(sender, e);
        }

        private void btFinish_Click(object sender, EventArgs e)
        {
            Presenter.btFinish_Click(sender, e, this);
            Close();
        }

        private Presenter.IQuestInterview Presenter { get; }
    }
}

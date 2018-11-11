﻿using System;
using System.Windows.Forms;
using Quest.Controls.Presenters;
using Quest.Core.Model;

namespace Quest.Controls.QuestInterview
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Presenter = new Presenters.QuestInterview(pnAnswers);
        }

        public MainForm(Questionnaire questionnaire) : this()
        {
            Presenter = new Presenters.QuestInterview(pnAnswers, questionnaire);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var result=Presenter.Load(this);
            if (result!=DialogResult.OK) Close();
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            Presenter.Next();
        }

        private void btFinish_Click(object sender, EventArgs e)
        {
            Presenter.Finish(this);
            Close();
        }

        private IQuestInterview Presenter { get; }
    }
}

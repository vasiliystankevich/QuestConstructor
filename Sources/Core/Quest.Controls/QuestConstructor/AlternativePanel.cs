using System;
using System.Windows.Forms;
using Quest.Controls.Presenters;
using Quest.Core.Model;
using Quest.Core.UI;
using Quest.Localizable;

namespace Quest.Controls.QuestConstructor
{
    public partial class AlternativePanel : UserControl, ILocalizableComponents
    {

        public AlternativePanel()
        {
            InitializeComponent();
            LocalizableComponents();
            Presenter = new Presenters.AlternativePanel(tbId, tbTitle, lbCondition);
        }

        public void Build(Questionnaire questionnaire, Core.Model.Quest quest, Alternative alt)
        {
            Presenter.Build(questionnaire, quest, alt);
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            Presenter.Delete();
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            Presenter.Up();
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            Presenter.Down();
        }

        private void tb_TextChanged(object sender, EventArgs e)
        {
            Presenter.UpdateObject();
        }

        private void lbCondition_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Presenter.ConditionClicked(this);
        }

        public void LocalizableComponents()
        {
            lbCondition.Text = I18NEngine.GetString("quest.controls", "questconstructor_alternativepanel_lbcondition_text");
        }

        private IAlternativePanel Presenter { get; }

        public event Action AlternativeListChanged
        {
            add => Presenter.AlternativeListChanged += value;
            remove => Presenter.AlternativeListChanged -= value;
        }

        public event Action Changed
        {
            add => Presenter.Changed += value;
            remove => Presenter.Changed -= value;
        }
    }
}

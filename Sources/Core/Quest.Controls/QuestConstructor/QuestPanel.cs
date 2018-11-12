using System;
using System.Windows.Forms;
using Quest.Controls.Presenters;
using Quest.Core.Model;
using Quest.Core.Services;
using Quest.Core.UI;
using Quest.Localizable;

namespace Quest.Controls.QuestConstructor
{
    public partial class QuestPanel : UserControl, ILocalizableComponents
    {
        public QuestPanel()
        {
            InitializeComponent();
            LocalizableComponents();
        }

        public void Build(Questionnaire questionnaire, Core.Model.Quest quest)
        {
            presenter.Build(questionnaire, quest, this);
        }

        private void UpdateObject()
        {
            if (updating > 0) return;

            quest.Id = tbId.Text;
            quest.Title = tbTitle.Text;
            quest.QuestType = (QuestType)cbQuestType.SelectedItem;

            Changed();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить вопрос?", "Удаление вопроса", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                new QuestionnaireManipulator().RemoveQuest(questionnaire, quest);
                QuestionnaireListChanged();
            }
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            new QuestionnaireManipulator().MoveQuest(questionnaire, quest, -1);
            QuestionnaireListChanged();
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            new QuestionnaireManipulator().MoveQuest(questionnaire, quest, +1);
            QuestionnaireListChanged();
        }

        private void tb_TextChanged(object sender, EventArgs e)
        {
            UpdateObject();
        }

        private void btAddAlt_Click(object sender, EventArgs e)
        {
            new QuestManipulator().AddNewAlt(quest);
            Build(questionnaire, quest);
        }

        private void lbCondition_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (quest.Condition == null)
                quest.Condition = new Condition();

            var form = new ConditionForm(questionnaire, quest.Condition);
            form.Changed += () => Changed();
            form.ShowDialog(this);

            Build(questionnaire, quest);
        }

        public void LocalizableComponents()
        {
            lbCondition.Text = I18NEngine.GetString("quest.controls", "questconstructor_questpanel_lbcondition_text");
            label3.Text = I18NEngine.GetString("quest.controls", "questconstructor_questpanel_label3_text");
        }

        private IQuestPanel presenter { get; set; }

        public event Action QuestionnaireListChanged = delegate { };
        public event Action Changed = delegate { };
    }
}

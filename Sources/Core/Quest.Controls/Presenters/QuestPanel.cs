using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quest.Core.Model;
using Quest.Core.Services;
using Quest.Localizable;

namespace Quest.Controls.Presenters
{
    public interface IQuestPanel
    {
        void Build(UserControl owner, Questionnaire questionnaire, Core.Model.Quest quest);
        void UpdateObject();
        void btDelete_Click();
        void btUp_Click();
        void btDown_Click();
        void tb_TextChanged();
        void btAddAlt_Click(UserControl owner);
        void lbCondition_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e);
        event Action QuestionnaireListChanged;
        event Action Changed;
    }

    public class QuestPanel : IQuestPanel
    {
        public void Build(Questionnaire questionnaire, Core.Model.Quest quest, UserControl owner)
        {
            this.questionnaire = questionnaire;
            this.quest = quest;

            updating++;

            var helper = new ControlHelper(owner);

            tbId.Text = quest.Id;
            tbTitle.Text = quest.Title;
            lbCondition.Text = quest.Condition?.ToString() ?? I18NEngine.GetString("quest.controls", "questconstructor_questpanel_lbcondition_text");

            cbQuestType.DataSource = Enum.GetValues(typeof(QuestType));
            cbQuestType.SelectedItem = quest.QuestType;

            pnAlternatives.Controls.Clear();
            foreach (var alt in quest)
            {
                var pn = new Controls.QuestConstructor.AlternativePanel();
                pn.Build(questionnaire, quest, alt);
                pn.AlternativeListChanged += () =>
                {
                    Build(questionnaire, quest);
                    Changed();
                };
                pn.Changed += () => Changed();
                pnAlternatives.Controls.Add(pn);
            }

            helper.ResumeDrawing();

            updating--;
        }

        public void UpdateObject()
        {
            if (updating > 0) return;

            quest.Id = tbId.Text;
            quest.Title = tbTitle.Text;
            quest.QuestType = (QuestType)cbQuestType.SelectedItem;

            Changed();
        }

        public void btDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить вопрос?", "Удаление вопроса", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                new QuestionnaireManipulator().RemoveQuest(questionnaire, quest);
                QuestionnaireListChanged();
            }
        }

        public void btUp_Click(object sender, EventArgs e)
        {
            new QuestionnaireManipulator().MoveQuest(questionnaire, quest, -1);
            QuestionnaireListChanged();
        }

        public void btDown_Click(object sender, EventArgs e)
        {
            new QuestionnaireManipulator().MoveQuest(questionnaire, quest, +1);
            QuestionnaireListChanged();
        }

        public void tb_TextChanged(object sender, EventArgs e)
        {
            UpdateObject();
        }

        public void btAddAlt_Click(object sender, EventArgs e, UserControl owner)
        {
            new QuestManipulator().AddNewAlt(quest);
            Build(questionnaire, quest);
        }

        public void lbCondition_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (quest.Condition == null)
                quest.Condition = new Condition();

            var form = new Controls.QuestConstructor.ConditionForm(questionnaire, quest.Condition);
            form.Changed += () => Changed();
            form.ShowDialog(this);

            Build(questionnaire, quest);
        }

        private Questionnaire questionnaire;
        private Core.Model.Quest quest;
        private int updating;

        private TextBox tbId { get; set; }
        private TextBox tbTitle { get; set; }
        private LinkLabel lbCondition { get; set; }

        public event Action QuestionnaireListChanged = delegate { };
        public event Action Changed = delegate { };
    }
}

using System;
using System.Windows.Forms;
using Quest.Core.Model;
using Quest.Core.Services;

namespace Quest.Controls.QuestConstructor
{
    public partial class QuestPanel : UserControl
    {
        public QuestPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Строим интерфейс
        /// </summary>
        public void Build(Questionnaire questionnaire, Core.Model.Quest quest)
        {
            this.questionnaire = questionnaire;
            this.quest = quest;

            updating++;

            var helper = new ControlHelper(this);

            tbId.Text = quest.Id;
            tbTitle.Text = quest.Title;
            lbCondition.Text = quest.Condition?.ToString() ?? "Если...";

            cbQuestType.DataSource = Enum.GetValues(typeof(QuestType));
            cbQuestType.SelectedItem = quest.QuestType;

            pnAlternatives.Controls.Clear();
            foreach (var alt in quest)
            {
                var pn = new AlternativePanel();
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

            var form = new ConditionForm();
            form.Build(questionnaire, quest.Condition);
            form.Changed += () => Changed();
            form.ShowDialog(this);

            Build(questionnaire, quest);
        }

        private Questionnaire questionnaire;
        private Core.Model.Quest quest;
        private int updating;

        public event Action QuestionnaireListChanged = delegate { };
        public event Action Changed = delegate { };
    }
}

using System;
using System.Windows.Forms;
using Quest.Core.Model;
using Quest.Core.Services;
using Quest.Core.UI;
using Quest.Localizable;

namespace Quest.Controls.QuestConstructor
{
    public partial class AlternativePanel : UserControl, ILocalizableComponents
    {

        public AlternativePanel()
        {
            InitializeComponent();
            this.SetCulture("ru-RU");
        }

        public void Build(Questionnaire questionnaire, Core.Model.Quest quest, Alternative alt)
        {
            this.quest = quest;
            this.alt = alt;
            this.questionnaire = questionnaire;

            updating++;

            tbId.Text = alt.Code.ToString();
            tbTitle.Text = alt.Title;
            lbCondition.Text = alt.Condition?.ToString() ?? I18NEngine.GetString("quest.controls", "questconstructor_alternativepanel_lbcondition_text");

            updating--;
        }

        private void UpdateObject()
        {
            if (updating > 0) return;

            alt.Code = int.Parse(tbId.Text);
            alt.Title = tbTitle.Text;

            Changed();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить альтернативу?", "Удаление альтернативы", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                new QuestManipulator().RemoveAlt(quest, alt);
                AlternativeListChanged();
            }
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            new QuestManipulator().MoveAlt(quest, alt, -1);
            AlternativeListChanged();
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            new QuestManipulator().MoveAlt(quest, alt, +1);
            AlternativeListChanged();
        }

        private void tb_TextChanged(object sender, EventArgs e)
        {
            UpdateObject();
        }

        private void lbCondition_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (alt.Condition == null)
                alt.Condition = new Condition();

            var form = new ConditionForm();
            form.Build(questionnaire, alt.Condition);
            form.Changed += () => Changed();
            form.ShowDialog(this);

            Build(questionnaire, quest, alt);
        }

        public void LocalizableComponents()
        {
            lbCondition.Text = I18NEngine.GetString("quest.controls", "questconstructor_alternativepanel_lbcondition_text");
        }

        private Core.Model.Quest quest;
        private Alternative alt;
        private Questionnaire questionnaire;
        private int updating;

        public event Action AlternativeListChanged = delegate { };
        public event Action Changed = delegate { };
    }
}

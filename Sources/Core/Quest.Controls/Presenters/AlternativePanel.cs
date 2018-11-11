using System;
using System.Windows.Forms;
using Quest.Core.Model;
using Quest.Core.Services;
using Quest.Localizable;

namespace Quest.Controls.Presenters
{
    public interface IAlternativePanel
    {
        void Build(Questionnaire questionnaire, Core.Model.Quest quest, Alternative alt);
        void UpdateObject();
        void Delete();
        void Up();
        void Down();
        void ConditionClicked(IWin32Window owner);

        event Action AlternativeListChanged;
        event Action Changed;
    }

    public class AlternativePanel : IAlternativePanel
    {
        public AlternativePanel(TextBox id, TextBox title, LinkLabel condition)
        {
            Id = id;
            Title = title;
            Condition = condition;
        }

        public void Build(Questionnaire questionnaire, Core.Model.Quest quest, Alternative alt)
        {
            this.quest = quest;
            this.alt = alt;
            this.questionnaire = questionnaire;

            updating++;

            Id.Text = alt.Code.ToString();
            Title.Text = alt.Title;
            Condition.Text = alt.Condition?.ToString() ?? I18NEngine.GetString("quest.controls", "questconstructor_alternativepanel_lbcondition_text");

            updating--;
        }

        public void UpdateObject()
        {
            if (updating > 0) return;

            alt.Code = int.Parse(Id.Text);
            alt.Title = Title.Text;

            Changed();
        }

        public void Delete()
        {
            if (MessageBox.Show("Удалить альтернативу?", "Удаление альтернативы", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                new QuestManipulator().RemoveAlt(quest, alt);
                AlternativeListChanged();
            }
        }

        public void Up()
        {
            new QuestManipulator().MoveAlt(quest, alt, -1);
            AlternativeListChanged();
        }

        public void Down()
        {
            new QuestManipulator().MoveAlt(quest, alt, +1);
            AlternativeListChanged();
        }

        public void ConditionClicked(IWin32Window owner)
        {
            if (alt.Condition == null)
                alt.Condition = new Condition();

            var form = new Controls.QuestConstructor.ConditionForm(questionnaire, alt.Condition);
            form.Changed += () => Changed();
            form.ShowDialog(owner);

            Build(questionnaire, quest, alt);
        }

        private Core.Model.Quest quest;
        private Alternative alt;
        private Questionnaire questionnaire;
        private int updating;

        private TextBox Id { get; }
        private TextBox Title { get; }
        private LinkLabel Condition { get; }

        public event Action AlternativeListChanged = delegate { };
        public event Action Changed = delegate { };
    }
}

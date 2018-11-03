using System;
using System.Linq;
using System.Windows.Forms;
using Quest.Core.Model;
using Quest.Core.Services;

namespace Quest.Controls.QuestInterview
{
    public partial class AnswerPanel : UserControl
    {
        public AnswerPanel()
        {
            InitializeComponent();
        }

        public void Build(InterviewManipulator interviewManipulator, Core.Model.Quest quest, Answer answer, bool readOnly)
        {
            this.interviewManipulator = interviewManipulator;
            this.quest = quest;
            this.answer = answer;

            lbQuestTitle.Text = quest.Title;

            pnMain.Controls.Clear();

            if (readOnly)
            {
                BuildReadOnlyAnswerInterface();
            }
            else
            { 
                switch (quest.QuestType)
                {
                    case QuestType.SingleAnswer: BuildSingleAnswerInterface(); break;
                    case QuestType.OpenQuestion: BuildOpenAnswerInterface(); break;
                }
            }
        }

        private void BuildSingleAnswerInterface()
        {
            var cb = new ComboBox();
            var alternatives = interviewManipulator.GetAllowedAlternatives().ToList();
            cb.DataSource = alternatives;
            cb.ValueMember = "Code";
            cb.DisplayMember = "Title";
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.Parent = pnMain;
            cb.SelectedValueChanged += (o, O) => OnValueSelected((int)cb.SelectedValue);//обрабатываем выбор
            if(alternatives.Count > 0)
                OnValueSelected(alternatives[0].Code);
        }

        private void BuildOpenAnswerInterface()
        {
            var tb = new TextBox();
            tb.Parent = pnMain;
            tb.TextChanged += (o, O) => OnValueSelected(tb.Text);
        }

        private void BuildReadOnlyAnswerInterface()
        {
            var lb = new Label();
            var alt = quest.FirstOrDefault(a => a.Code == answer.AlternativeCode);
            lb.Text = alt?.Title + " " + answer.Text;
            lb.Parent = pnMain;
        }

        private void OnValueSelected(int alternativeCode)
        {
            answer.AlternativeCode = alternativeCode; 
            Changed();
        }

        private void OnValueSelected(string text)
        {
            answer.Text = text; 
            Changed();
        }

        private Core.Model.Quest quest;
        private Answer answer;
        private InterviewManipulator interviewManipulator;

        public event Action Changed = delegate { };

    }
}

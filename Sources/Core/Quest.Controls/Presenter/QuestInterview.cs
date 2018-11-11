using System;
using System.Linq;
using System.Windows.Forms;
using Quest.Controls.QuestInterview;
using Quest.Core.Helpers;
using Quest.Core.Model;
using Quest.Core.Services;

namespace Quest.Controls.Presenter
{
    public interface IQuestInterview:IDisposable
    {
        void Build();
        DialogResult OnLoad(EventArgs e, IWin32Window owner);
        void btNext_Click(object sender, EventArgs e);
        void btFinish_Click(object sender, EventArgs e, IWin32Window owner);
    }

    public class QuestInterview : IQuestInterview
    {
        public QuestInterview(FlowLayoutPanel pnAnswers, Questionnaire questionnaire):this(pnAnswers)
        {
            this.questionnaire = questionnaire;
        }

        public QuestInterview(FlowLayoutPanel pnAnswers)
        {
            this.pnAnswers = pnAnswers;
        }

        public void Build()
        {
            //создаем хелпер отрисовки, останавливаем отрисовку
            //var helper = new ControlHelper(pnAnswers);
            pnAnswers.SuspendLayout();
            //очищаем панель ответов
            pnAnswers.Controls.Clear();

            //отображаем уже отвеченные вопросы
            foreach (var answer in interview.PassedAnswers)
            {
                //создаем панель ответа
                var pn = new AnswerPanel();
                //строим
                pn.Build(interviewManipulator, questionnaire.First(q => q.Id == answer.QuestId), answer, true);
                //добавляем на форму
                pn.Parent = pnAnswers;
            }

            //отображаем вопрос, на который нужно ответить
            if (interview.CurrentAnswer != null)
            {
                //создаем панель ответа
                var pn = new AnswerPanel();
                //строим
                pn.Build(interviewManipulator, questionnaire.First(q => q.Id == interview.CurrentAnswer.QuestId), interview.CurrentAnswer, false);
                //добавляем на форму
                pn.Parent = pnAnswers;
            }

            //добавляем кнопку "далее"
            //btNext.Parent = interview.IsFinished ? null : pnAnswers;
            //btFinish.Parent = interview.IsFinished ? pnAnswers : null;

            //восстанавливаем отрисовку
            //helper.ResumeDrawing();
            pnAnswers.ResumeLayout(false);
        }

        public DialogResult OnLoad(EventArgs e, IWin32Window owner)
        {
            //создаем новую анкету
            anketa = new Anketa();

            //запрашиваем опросник, если он не задан
            if (questionnaire == null)
            {
                var ofd = new OpenFileDialog() { Filter = "Опросник|*.q", Title = "Выберите опросник" };
                var dialogResult = ofd.ShowDialog(owner);
                if (dialogResult != DialogResult.OK) return DialogResult.Cancel;
                questionnaire = SaverLoader.Load<Questionnaire>(ofd.FileName);
            }

            //создаем процесс опроса (интервью)
            interview = new Interview(questionnaire, anketa);
            //создаем манипулятор для интервью
            interviewManipulator = new InterviewManipulator(interview);

            //переходим к первому вопросу
            interviewManipulator.GoToNextQuestion();

            //строим интерфейс
            Build();
            return DialogResult.OK;
        }

        public void btNext_Click(object sender, EventArgs e)
        {
            //переходим к следующему вопросу
            interviewManipulator.GoToNextQuestion();
            //строим интерфейс
            Build();
        }

        public void btFinish_Click(object sender, EventArgs e, IWin32Window owner)
        {
            //предлагаем сохранить анкету
            if (MessageBox.Show("Сохранить анкету?", "Сохранение анкеты", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                var sfd = new SaveFileDialog() { Filter = "Анкета|*.a", FileName = Guid.NewGuid().ToString() };
                if (sfd.ShowDialog(owner) == DialogResult.OK)
                {
                    //сохраняем анкету
                    SaverLoader.Save(anketa, sfd.FileName);
                }
            }

        }

        //интервью
        private Interview interview;

        //функционал интервью
        private InterviewManipulator interviewManipulator;

        //текущий опросник
        private Questionnaire questionnaire;

        //текущая анкета
        private Anketa anketa;
        private FlowLayoutPanel pnAnswers { get; }

        public void Dispose()
        {
        }
    }
}

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
        DialogResult Load(IWin32Window owner);
        void Next();
        void Finish(IWin32Window owner);
    }

    public class QuestInterview : IQuestInterview
    {
        public QuestInterview(FlowLayoutPanel pnAnswers, Questionnaire questionnaire):this(pnAnswers)
        {
            Questionnaire = questionnaire;
        }

        public QuestInterview(FlowLayoutPanel pnAnswers)
        {
            PnAnswers = pnAnswers;
        }

        public void Build()
        {
            //создаем хелпер отрисовки, останавливаем отрисовку
            //var helper = new ControlHelper(pnAnswers);
            PnAnswers.SuspendLayout();
            //очищаем панель ответов
            PnAnswers.Controls.Clear();

            //отображаем уже отвеченные вопросы
            foreach (var answer in Interview.PassedAnswers)
            {
                //создаем панель ответа
                var pn = new AnswerPanel();
                //строим
                pn.Build(InterviewManipulator, Questionnaire.First(q => q.Id == answer.QuestId), answer, true);
                //добавляем на форму
                pn.Parent = PnAnswers;
            }

            //отображаем вопрос, на который нужно ответить
            if (Interview.CurrentAnswer != null)
            {
                //создаем панель ответа
                var pn = new AnswerPanel();
                //строим
                pn.Build(InterviewManipulator, Questionnaire.First(q => q.Id == Interview.CurrentAnswer.QuestId), Interview.CurrentAnswer, false);
                //добавляем на форму
                pn.Parent = PnAnswers;
            }

            //добавляем кнопку "далее"
            //btNext.Parent = interview.IsFinished ? null : pnAnswers;
            //btFinish.Parent = interview.IsFinished ? pnAnswers : null;

            //восстанавливаем отрисовку
            //helper.ResumeDrawing();
            PnAnswers.ResumeLayout(false);
        }

        public DialogResult Load(IWin32Window owner)
        {
            //создаем новую анкету
            Anketa = new Anketa();

            //запрашиваем опросник, если он не задан
            if (Questionnaire == null)
            {
                var ofd = new OpenFileDialog() { Filter = "Опросник|*.q", Title = "Выберите опросник" };
                var dialogResult = ofd.ShowDialog(owner);
                if (dialogResult != DialogResult.OK) return DialogResult.Cancel;
                Questionnaire = SaverLoader.Load<Questionnaire>(ofd.FileName);
            }

            //создаем процесс опроса (интервью)
            Interview = new Interview(Questionnaire, Anketa);
            //создаем манипулятор для интервью
            InterviewManipulator = new InterviewManipulator(Interview);

            //переходим к первому вопросу
            InterviewManipulator.GoToNextQuestion();

            //строим интерфейс
            Build();
            return DialogResult.OK;
        }

        public void Next()
        {
            InterviewManipulator.GoToNextQuestion();
            Build();
        }

        public void Finish(IWin32Window owner)
        {
            //предлагаем сохранить анкету
            if (MessageBox.Show("Сохранить анкету?", "Сохранение анкеты", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                var sfd = new SaveFileDialog() { Filter = "Анкета|*.a", FileName = Guid.NewGuid().ToString() };
                if (sfd.ShowDialog(owner) == DialogResult.OK)
                {
                    //сохраняем анкету
                    SaverLoader.Save(Anketa, sfd.FileName);
                }
            }
        }


        public void Dispose()
        {
        }
        private Interview Interview { get; set; }
        private InterviewManipulator InterviewManipulator { get; set; }
        private Questionnaire Questionnaire { get; set; }
        private Anketa Anketa { get; set; }
        private FlowLayoutPanel PnAnswers { get; }
    }
}

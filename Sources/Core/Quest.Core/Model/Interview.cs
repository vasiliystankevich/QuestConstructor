using System.Collections.Generic;

namespace Quest.Core.Model
{
    /// <summary>
    /// Процесс прохождения интервью
    /// </summary>
    public class Interview
    {
        internal Questionnaire questionnaire { get; }
        internal Anketa anketa { get; }

        /// <summary>
        /// Ответы, уже данные респондентом
        /// </summary>
        public List<Answer> PassedAnswers { get; set; } = new List<Answer>();

        /// <summary>
        /// Текущий вопрос, на который отвечает респондент в данный момент
        /// </summary>
        public Answer CurrentAnswer { get; set; }

        /// <summary>
        /// Интервью завершено?
        /// </summary>
        public bool IsFinished { get; internal set; }

        public Interview(Questionnaire questionnaire, Anketa anketa)
        {
            this.questionnaire = questionnaire;
            this.anketa = anketa;
        }
    }
}

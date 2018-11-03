using System.Collections.Generic;

namespace QuestCore
{
    /// <summary>
    /// Процесс прохождения интервью
    /// </summary>
    public class Interview
    {
        internal Questionnaire questionnaire { get; private set; }
        internal Anketa anketa { get; private set; }

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

using System.Collections.Generic;

namespace Quest.Core.Model
{
    public class Interview
    {
        public Interview(Questionnaire questionnaire, Anketa anketa)
        {
            this.questionnaire = questionnaire;
            this.anketa = anketa;
        }

        public Questionnaire questionnaire { get; }
        public Anketa anketa { get; }
        public List<Answer> PassedAnswers { get; set; } = new List<Answer>();
        public Answer CurrentAnswer { get; set; }
        public bool IsFinished { get; internal set; }

    }
}

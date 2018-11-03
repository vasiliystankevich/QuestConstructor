using System.Collections.Generic;
using System.Linq;
using Quest.Core.Model;

namespace Quest.Core.Services
{
    public class InterviewManipulator
    {
        private Interview interview;

        public InterviewManipulator(Interview interview)
        {
            this.interview = interview;
        }

        public void GoToNextQuestion()
        {
            if (interview.IsFinished) return;

            var currentQuestIndex = interview.CurrentAnswer == null ? -1 : interview.questionnaire.FindIndex(q => q.Id == interview.CurrentAnswer.QuestId);

            for (int i = currentQuestIndex + 1; i < interview.questionnaire.Count; i++)
            {
                var condition = interview.questionnaire[i].Condition;

                var clalculator = new ConditionCalculator();
                if (clalculator.Calculate(interview.anketa, condition))
                {
                    var quest = interview.questionnaire[i];
                    if(interview.CurrentAnswer != null)
                        interview.PassedAnswers.Add(interview.CurrentAnswer);
                    interview.CurrentAnswer = new Answer { QuestId = quest.Id };
                    interview.anketa.Add(interview.CurrentAnswer);
                    return;
                }
            }

            if (interview.CurrentAnswer != null)
            {
                interview.PassedAnswers.Add(interview.CurrentAnswer);
                interview.CurrentAnswer = null;
            }
            interview.IsFinished = true;
        }

        public IEnumerable<Alternative> GetAllowedAlternatives()
        {
            if (interview.IsFinished) yield break;
            if (interview.CurrentAnswer == null) yield break;

            var quest = interview.questionnaire.First(q => q.Id == interview.CurrentAnswer.QuestId);

            var clalculator = new ConditionCalculator();
            foreach (var alt in quest)
            if (clalculator.Calculate(interview.anketa, alt.Condition))
                yield return alt;
        }
    }
}
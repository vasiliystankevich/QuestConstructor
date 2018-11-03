using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Quest.Core.Model;

namespace Quest.Core.Services
{
    public class ConditionCalculator
    {
        public bool Calculate(Anketa anketa, Condition condition)
        {
            if (condition == null || string.IsNullOrEmpty(condition.Expression))
                return true;

            var expression = Regex.Replace(condition.Expression, QuestNamePattern,
                m =>
                {
                    var questId = m.Value;
                    var answer = anketa.FirstOrDefault(a => a.QuestId == questId);
                    if (answer != null)
                    {
                        if (string.IsNullOrEmpty(answer.Text))
                            return answer.AlternativeCode.ToString();

                        return "'" + answer.Text + "'";
                    }
                    return "0";
                });

            return (bool) new DataTable().Compute(expression, null);
        }

        public void Check(Questionnaire questionnaire, string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return;

            foreach(Match m in Regex.Matches(expression, QuestNamePattern))
                if (questionnaire.All(q => q.Id != m.Value))
                    throw new Exception("Вопрос " + m.Value + " не найден");

            Calculate(new Anketa(), new Condition {Expression = expression});
        }

        private const string QuestNamePattern = @"\b[A-Z]\d+\b";
    }
}

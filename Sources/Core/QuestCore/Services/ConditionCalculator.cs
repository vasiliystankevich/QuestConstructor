using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuestCoreNS
{
    /// <summary>
    /// Вычисляет условия
    /// </summary>
    public class ConditionCalculator
    {
        private const string QuestNamePattern = @"\b[A-Z]\d+\b";

        public bool Calculate(Anketa anketa, Condition condition)
        {
            if (condition == null || string.IsNullOrEmpty(condition.Expression))
                return true;

            //заменяем имена вопросов на значения
            var expression = Regex.Replace(condition.Expression, QuestNamePattern,
                (m) =>
                {
                    var questId = m.Value;
                    //ищем ответ
                    var answer = anketa.FirstOrDefault(a => a.QuestId == questId);
                    if (answer != null)
                    {
                        //заменяем имя вопроса на ответ
                        if (string.IsNullOrEmpty(answer.Text))
                            return answer.AlternativeCode.ToString();

                        return "'" + answer.Text + "'";
                    }
                    return "0";//код отсутствия ответа
                });

            //вычислем выражение
            return (bool) new DataTable().Compute(expression, null);
        }

        public void Check(Questionnaire questionnaire, string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return;

            //проверяем имена вопросов
            foreach(Match m in Regex.Matches(expression, QuestNamePattern))
                if (questionnaire.All(q => q.Id != m.Value))
                    throw new Exception("Вопрос " + m.Value + " не найден");

            //проверяем синтаксис
            Calculate(new Anketa(), new Condition {Expression = expression});
        }
    }
}

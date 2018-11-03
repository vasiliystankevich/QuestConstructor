using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestCoreNS
{
    /// <summary>
    /// Проверяет корректность опросника.
    /// Генерирует исключение с текстом, описывающим суть проблемы.
    /// </summary>
    public class QuestionnaireValidator
    {
        public void Validate(Questionnaire questionnaire)
        {
            //проверяем уникальность имен вопросов
            var names = new HashSet<string>();
            foreach(var q in questionnaire)
            if(!names.Add(q.Id))
                throw new Exception("Дублируется имя вопроса " + q.Id);

            //проверяем уникальность кодов альтернатив и их число
            foreach (var quest in questionnaire)
            {
                var codes = new HashSet<int>();
                foreach (var a in quest)
                if (!codes.Add(a.Code))
                    throw new Exception("В вопросе " + quest.Id + " дублируется код альтернативы " + a.Code);
                if(quest.Count == 0)
                    throw new Exception("В вопросе " + quest.Id + " нет альтернатив");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using Quest.Core.Model;

namespace Quest.Core.Services
{
    public class QuestionnaireValidator
    {
        public void Validate(Questionnaire questionnaire)
        {
            var names = new HashSet<string>();
            foreach(var q in questionnaire)
            if(!names.Add(q.Id))
                throw new Exception("Дублируется имя вопроса " + q.Id);

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

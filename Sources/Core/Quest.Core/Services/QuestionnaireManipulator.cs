using System.Linq;
using Quest.Core.Helpers;
using Quest.Core.Model;

namespace Quest.Core.Services
{
    public class QuestionnaireManipulator
    {
        public static Model.Quest AddNewQuest(Questionnaire questionnaire, string title)
        {
            var counter = 1;
            var name = DefaultQuestPrefix + counter;

            while (questionnaire.Any(q => q.Id == name))
            {
                counter++;
                name = DefaultQuestPrefix + counter;
            }

            var quest = new Model.Quest {Id = name, Title = title };
            questionnaire.Add(quest);
            return quest;
        }

        public void RemoveQuest(Questionnaire questionnaire, Model.Quest quest)
        {
            questionnaire.Remove(quest);
        }

        public void MoveQuest(Questionnaire questionnaire, Model.Quest quest, int dir)
        {
            ListHelper.MoveElement(questionnaire, quest, dir);
        }

        private const string DefaultQuestPrefix = "A";
    }
}

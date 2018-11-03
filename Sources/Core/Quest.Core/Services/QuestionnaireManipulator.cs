using System.Linq;

namespace Quest.Core
{
    /// <summary>
    /// Производит манипуляции с опросником
    /// </summary>
    public class QuestionnaireManipulator
    {
        private const string DefaultQuestPrefix = "A";

        /// <summary>
        /// Добавить новый вопрос
        /// </summary>
        public global::Quest.Core.Quest AddNewQuest(Questionnaire questionnaire)
        {
            //подбираем уникальное имя вопроса
            var counter = 1;
            var name = DefaultQuestPrefix + counter;

            while (questionnaire.Any(q => q.Id == name))//увеличиваем счетчик, пока не найдем имени, которого еще нет в опроснике
            {
                counter++;
                name = DefaultQuestPrefix + counter;
            }
            //
            var quest = new global::Quest.Core.Quest() {Id = name, Title = "Текст вопроса"};
            questionnaire.Add(quest);
            return quest;
        }

        /// <summary>
        /// Удалить вопрос
        /// </summary>
        public void RemoveQuest(Questionnaire questionnaire, global::Quest.Core.Quest quest)
        {
            questionnaire.Remove(quest);
        }

        /// <summary>
        /// Перемещение вопроса
        /// </summary>
        public void MoveQuest(Questionnaire questionnaire, global::Quest.Core.Quest quest, int dir)
        {
            ListHelper.MoveElement(questionnaire, quest, dir);
        }
    }
}

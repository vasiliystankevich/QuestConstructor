using System.Linq;
using Quest.Core.Helpers;
using Quest.Core.Model;

namespace Quest.Core.Services
{
    public class QuestManipulator
    {
        public Alternative AddNewAlt(Model.Quest quest)
        {
            var code = 1;
            while (quest.Any(a => a.Code == code))
                code++;

            var alt = new Alternative() { Code = code, Title = "Вариант "  + code };
            quest.Add(alt);
            return alt;
        }

        public void RemoveAlt(Model.Quest quest, Alternative alt)
        {
            quest.Remove(alt);
        }

        public void MoveAlt(Model.Quest quest, Alternative alt, int dir)
        {
            ListHelper.MoveElement(quest, alt, dir);
        }
    }
}
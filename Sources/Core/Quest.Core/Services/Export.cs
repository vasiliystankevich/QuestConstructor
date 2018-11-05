using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Quest.Core.Model;

namespace Quest.Core.Services
{
    public class Export
    {
        public static void ExportToCsv(List<Anketa> anketas, string fileName)
        {
            var questNames = anketas.SelectMany(a => a).Select(a => a.QuestId).Distinct().ToArray();
            using (var sw = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                sw.WriteLine(string.Join(Separator, questNames));
                foreach (var anketa in anketas)
                {
                    var nameToAnswer = new Dictionary<string, string>();
                    foreach (var answer in anketa)
                        nameToAnswer[answer.QuestId] = answer.ToString();

                    for (int i = 0; i < questNames.Length; i++)
                    {
                        if (i > 0) sw.Write(Separator);
                        if (nameToAnswer.ContainsKey(questNames[i]))
                            sw.Write(nameToAnswer[questNames[i]]);
                    }

                    sw.WriteLine();
                }
            }
        }

        const string Separator = ";";
    }
}

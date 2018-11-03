using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Quest.Core.Model;

namespace Quest.Core.Services
{
    public class Export
    {
        public void ExportToCSV(IEnumerable<Anketa> anketas, string fileName)
        {
            var questNames = anketas.SelectMany(a => a).Select(a => a.QuestId).Distinct().ToArray();
            using (var sw = new StreamWriter(fileName, false, Encoding))
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

        public string Separator { get; set; } = ";";
        public Encoding Encoding { get; set; } = Encoding.UTF8;
    }
}

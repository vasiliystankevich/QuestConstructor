using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestCoreNS
{
    /// <summary>
    /// Условие, которое может возвращать true или false в зависимости от ответов в Anketa
    /// </summary>
    [Serializable]
    public class Condition
    {
        public string Expression { get; set; }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Expression) ? "Если..." : Expression;
        }
    }
}

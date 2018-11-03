using System;

namespace Quest.Core.Model
{
    [Serializable]
    public class Condition
    {
        public override string ToString()
        {
            return string.IsNullOrEmpty(Expression) ? "Если..." : Expression;
        }
        public string Expression { get; set; }
    }
}

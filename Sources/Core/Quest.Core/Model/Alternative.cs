using System;

namespace Quest.Core.Model
{
    [Serializable]
    public class Alternative
    {
        public int Code { get; set; }
        public string Title { get; set; }
        public Condition Condition { get; set; }
    }
}

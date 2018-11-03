using System;
using System.Collections.Generic;

namespace Quest.Core.Model
{
    [Serializable]
    public class Quest : List<Alternative>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public QuestType QuestType { get; set; }
        public Condition Condition { get; set; }
    }
}

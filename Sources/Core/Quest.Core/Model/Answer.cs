namespace Quest.Core.Model
{
    public class Answer
    {
        public override string ToString()
        {
            return Text ?? AlternativeCode.ToString();
        }

        public string QuestId { get; set; }
        public int? AlternativeCode { get; set; }
        public string Text { get; set; }

    }
}

namespace Quest.Core.Model
{
    public class Answer
    {
        /// <summary>
        /// Идентификатор вопроса
        /// </summary>
        public string QuestId { get; set; }

        /// <summary>
        /// Код выбранной альтернативы
        /// </summary>
        public int? AlternativeCode { get; set; }

        /// <summary>
        /// Текст ответа (для открытых вопросов)
        /// </summary>
        public string Text { get; set; }

        public override string ToString()
        {
            return Text ?? AlternativeCode.ToString();
        }
    }
}

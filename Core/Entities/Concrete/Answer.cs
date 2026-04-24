namespace Core.Entities.Concrete
{
    public class Answer: IEntity
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public bool IsCorrect { get; set; }
        public string AnswerText { get; set; }
    }
}

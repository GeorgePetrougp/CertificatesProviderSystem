namespace WebApp.DTO_Models.Questions
{
    public class QuestionDTO
    {
        public int QuestionId { get; set; }
        public string Display { get; set; }
        public int Points { get; set; }
        public string? Status { get; set; }
        public QuestionDifficultyDTO? Difficulty { get; set; }
        public List<QuestionPossibleAnswersDTO>? PossibleAnswers { get; set; }
        
    }
}

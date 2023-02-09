namespace WebApp.DTO_Models.Questions
{
    public class QuestionDTO
    {
        public int QuestionId { get; set; }
        public string QuestionDisplay { get; set; }
        public List<QuestionPossibleAnswersDTO> PossibleAnswers { get; set; }
    }
}

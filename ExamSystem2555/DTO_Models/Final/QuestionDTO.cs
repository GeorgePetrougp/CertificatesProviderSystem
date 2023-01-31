namespace WebApp.DTO_Models.Final
{
    public class QuestionDTO
    {
        public int QuestionId { get; set; }
        public string QuestionDisplay { get; set; }
        public List<QuestionPossibleAnswersDTO> PossibleAnswers { get; set; }
    }
}

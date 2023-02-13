namespace WebApp.DTO_Models.Questions
{
    public class QuestionPossibleAnswersDTO
    {
        public int QuestionPossibleAnswerId { get; set; }
        public string PossibleAnswer { get; set;}
        public bool IsCorrect { get; set;}
        public QuestionPossibleAnswersDTO()
        {
            //PossibleAnswer= string.Empty;
        }
 
    }
}

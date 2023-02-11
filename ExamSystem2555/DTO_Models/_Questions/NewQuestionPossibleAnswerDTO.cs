using MyDatabase.Models;

namespace WebApp.DTO_Models._Questions
{
    public class NewQuestionPossibleAnswerDTO
    {
        public int QuestionPossibleAnswerId { get; set; }
        public string PossibleAnswer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
using MyDatabase.Models;

namespace WebApp.DTO_Models
{
    public class QuestionAnswerView
    {
        public int QuestionPossibleAnswerId { get; set; }
        public string PossibleAnswer { get; set; }
        //public List<QuestionPossibleAnswer> questionPossibleAnswers { get; set; }
        public bool IsCorrect { get; set; }

    }
}

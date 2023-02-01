using MyDatabase.Models;

namespace WebApp.DTO_Models.Final
{
    public class QuestionView
    {
        public QuestionDTO Question { get; set; }
        public List<QuestionPossibleAnswersDTO> PossibleAnswers { get; set; }

    }
}

using MyDatabase.Models;

namespace WebApp.DTO_Models
{
    public class AnswerView
    {
        public List<int> SelectedPossibleAnswerIds { get; set; }
        public List<QuestionPossibleAnswer> QuestionPossibleAnswers { get; set; }


    }
}

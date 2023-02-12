using MyDatabase.Models;

namespace WebApp.DTO_Models._Questions
{
    public class NewQuestionDTO
    {
        public int QuestionId { get; set; }
        public string Display { get; set; }
        public int Points { get; set; }
        public string? Status { get; set; }
        public IList<NewQuestionPossibleAnswerDTO>? QuestionPossibleAnswers { get; set; }
    }
}

using MyDatabase.Models;

namespace WebApp.DTO_Models.Questions
{
    public class CreateQuestionDTO
    {
        public int QuestionId { get; set; }
        public string Display { get; set; }
        public int Points { get; set; }
        public string? Status { get; set; }
        public QuestionDifficultyDTO? Difficulty { get; set; }
        public List<QuestionPossibleAnswersDTO>? PossibleAnswers { get; set; }

        public CreateQuestionDTO()
        {
            //Display = string.Empty;
            PossibleAnswers = Enumerable.Repeat(new QuestionPossibleAnswersDTO(), 4).ToList();
        }

    }
}

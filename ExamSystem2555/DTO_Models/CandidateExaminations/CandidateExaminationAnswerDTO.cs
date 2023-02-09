using MyDatabase.Models;
using WebApp.DTO_Models.Questions;

namespace WebApp.DTO_Models.CandidateExaminations
{
    public class CandidateExaminationAnswerDTO
    {
        public int ExamCandidateAnswerId { get; set; }
        public int SelectedAnswer { get; set; }
        public int CorrectAnswer { get; set; }
        public bool IsSelectedAnswer { get; set; }
        public QuestionDTO Question { get; set; }

    }
}

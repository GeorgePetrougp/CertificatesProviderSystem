using MyDatabase.Models;

namespace WebApp.DTO_Models
{
    public class ExaminationView
    {
        public ExamQuestionView Question { get; set; }
        public ExamAnswerView AnswerA { get; set; }
        public ExamAnswerView AnswerB { get; set; }
        public ExamAnswerView AnswerC { get; set; }
        public ExamAnswerView AnswerD { get; set; }


    }
}

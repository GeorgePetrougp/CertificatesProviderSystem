using MyDatabase.Models;

namespace WebApp.DTO_Models
{
    public class AnswerView
    {
        public int QAnswerId { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }

    }
}

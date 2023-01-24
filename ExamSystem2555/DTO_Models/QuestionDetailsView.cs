using MyDatabase.Models;

namespace WebApp.DTO_Models
{
    public class QuestionDetailsView
    {
        public int QuestionDetailsViewId { get; set; }
        public string QuestionDisplay { get; set; }
        public string QuestionDifficulty { get; set; }
        public List<QuestionPossibleAnswer> Answers { get; set; }
        public List<Topic> Topics { get; set; }
        public List<Certificate> Certificates { get; set; }

    }
}

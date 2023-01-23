namespace MyDatabase.Models
{
    public class QuestionDifficulty
    {
        public int QuestionDifficultyId { get; set; }
        public string Difficulty { get; set; }
        public virtual ICollection<Question> Questions { get; set; }

    }
}
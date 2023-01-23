namespace MyDatabase.Models
{
    public class QuestionPossibleAnswer
    {
        public int QuestionPossibleAnswerId { get; set; }
        public string PossibleAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public virtual Question Question { get; set; }
    }
}
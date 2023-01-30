using System.Text.Json.Serialization;

namespace MyDatabase.Models
{
    public class QuestionPossibleAnswer
    {
        public int QuestionPossibleAnswerId { get; set; }
        public string PossibleAnswer { get; set; }
        public bool IsCorrect { get; set; }
        [JsonIgnore]
        public virtual Question Question { get; set; }
    }
}
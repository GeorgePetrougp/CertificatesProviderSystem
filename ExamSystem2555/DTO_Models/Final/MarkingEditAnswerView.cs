namespace WebApp.DTO_Models.Final
{
    public class MarkingEditAnswerView
    {
        public int? CandidateAnswerId { get; set; }
        public int SelectedAnswer { get; set; }
        public int CorrectAnswer { get; set; }
        public QuestionDTO Question { get; set; }
    }
}

namespace WebApp.DTO_Models.Final
{
    public class ExaminationQuestionView
    {
        public int CandidateExamId { get; set; }
        public int CurrentIndex { get; set; }
        public QuestionDTO[] Questions { get; set; }
        public int SelectedAnswerId { get; set; }
        
    }
}

using WebApp.DTO_Models._Questions;

namespace WebApp.DTO_Models.Final
{
    public class ExaminationQuestionView
    {
        public int CandidateExamId { get; set; }
        public int ExaminationId { get; set; }
        public int CurrentIndex { get; set; }
        public List<NewQuestionDTO> Questions { get; set; }
        public int SelectedAnswerId { get; set; }
        
    }
}

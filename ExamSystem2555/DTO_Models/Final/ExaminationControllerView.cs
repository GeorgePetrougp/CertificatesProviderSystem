using MyDatabase.Models;

namespace WebApp.DTO_Models.Final
{
    public class ExaminationControllerView
    {
        public int CandidateExamId { get; set; }
        public string ExamCode { get; set; }
        public DateTime ExamDate { get; set; }
        public virtual Candidate? Candidate { get; set; }
        public virtual Examination? Examination { get; set; }
        public DateTime ResultIssueDate { get; set; }
        public string ResultLabel { get; set; }
        public int CandidateTotalScore { get; set; }

        public ExaminationControllerView()
        {

        }

    }
}

using MyDatabase.Models;

namespace WebApp.DTO_Models.CandidateExaminations
{
    public class CandidateExaminationsDTO
    {
        public int CandidateExaminationId { get; set; }
        public string ExamCode { get; set; }
        public DateTime ExamDate { get; set; }
        public Candidate? Candidate { get; set; }
        public Examination? Examination { get; set; }
        public CandidateExaminationResultsDTO? CandidateExamResults { get; set; }
    }
}

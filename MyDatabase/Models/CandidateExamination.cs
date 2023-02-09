using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDatabase.Models
{
    public class CandidateExamination
    {
        public int CandidateExaminationId { get; set; }
        public string ExamCode { get; set; }
        public DateTime ExamDate { get; set; }
        public virtual Candidate? Candidate { get; set; }
        public virtual Examination? Examination { get; set; }
        public virtual ICollection<CandidateExaminationAnswer> ExamCandidateAnswers { get; set; }
        public virtual CandidateExaminationResult? CandidateExamResults { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDatabase.Models
{
    public class CandidateExam
    {
        public int CandidateExamId { get; set; }
        public string ExamCode { get; set; }
        public DateTime ExamDate { get; set; }
        public DateTime ResultIssueDate { get; set; }
        public string ResultLabel { get; set; }

        public int CandidateTotalScore { get; set; }
        //public double FinalScorePercentage { get; set; }
        //public double CertificateRequiredPercentage { get; set; }

        public virtual Candidate? Candidate { get; set; }
        public virtual Examination? Examination { get; set; }
        public virtual ICollection<ExamCandidateAnswer> ExamCandidateAnswers { get; set; }
    }
}

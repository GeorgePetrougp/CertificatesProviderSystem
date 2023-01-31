using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDatabase.Models
{
    public class CandidateExamResults
    {
        public int CandidateExamResultsId { get; set; }
        public DateTime ResultIssueDate { get; set; }
        public string ResultLabel { get; set; }

        public int CandidateTotalScore { get; set; }
        public int CandidateExamId { get; set; }
        public virtual CandidateExam CandidateExam { get; set; }
    }
}

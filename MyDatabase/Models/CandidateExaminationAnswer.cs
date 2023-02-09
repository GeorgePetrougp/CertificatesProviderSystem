using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDatabase.Models
{
    public class CandidateExaminationAnswer
    {
        public int ExamCandidateAnswerId { get; set; }
        public int SelectedAnswer { get; set; }
        public int CorrectAnswer { get; set; }
        public virtual CandidateExamination? CandidateExam { get; set; }
        public virtual CertificateTopicQuestion? CertificateTopicQuestion { get; set; }
    }
}

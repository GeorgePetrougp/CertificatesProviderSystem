using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDatabase.Models
{
    public class CandidateExaminationAnswer
    {
        public int CandidateExaminationAnswerId { get; set; }
        public int SelectedAnswer { get; set; }
        public int CorrectAnswer { get; set; }
        public int PointsAssignedDuringExamination { get; set; }
        public int PointsAssignedAfterMarking { get; set; }
        public virtual CandidateExamination? CandidateExam { get; set; }
        public virtual CertificateTopicQuestion? CertificateTopicQuestion { get; set; }
    }
}

using System;

namespace MyDatabase.Models
{
    public class Examination
    {
        public int ExaminationId { get; set; } 
        public virtual Certificate Certificate { get; set; }
        public ICollection<CandidateExamination> CandidateExams { get; set; }
        public virtual ICollection<ExaminationQuestion> ExamQuestions { get; set; }

    }
}

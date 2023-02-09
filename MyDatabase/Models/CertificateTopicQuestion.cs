namespace MyDatabase.Models
{
    public class CertificateTopicQuestion
    {
        public int CertificateTopicQuestionId { get; set; }
        public virtual CertificateTopic? CertificateTopic { get; set; }
        public virtual TopicQuestion? TopicQuestion { get; set; }
        public virtual ICollection<ExaminationQuestion> ExamQuestions { get; set; }
        public virtual ICollection<CandidateExaminationAnswer> ExamCandidateAnswers { get; set; }

    }
}
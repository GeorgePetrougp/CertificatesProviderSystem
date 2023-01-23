namespace MyDatabase.Models
{
    public class CertificateTopicQuestion
    {
        public int CertificateTopicQuestionId { get; set; }
        public virtual Certificate? Certificate { get; set; }
        public virtual TopicQuestion? TopicQuestion { get; set; }
        public virtual ICollection<ExaminationQuestion> ExamQuestions { get; set; }
        public virtual ICollection<ExamCandidateAnswer> ExamCandidateAnswers { get; set; }

    }
}
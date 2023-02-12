using MyDatabase.Models;

namespace WebApp.DTO_Models.CertificateExaminations
{
    public class CertificateExaminationQuestion
    {
        public int ExaminationQuestionId { get; set; }
        public CertificateTopicQuestion CertificateTopicQuestion { get; set; }

    }
}

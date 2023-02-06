using MyDatabase.Models;

namespace WebApp.DTO_Models.Final
{
    public class CertificateExaminationView
    {
        public int ExaminationId { get; set; }
        public int CertificateId { get; set; }
        public string CertificateTitle { get; set; }
        public List<CertificateExaminationQuestionDTO> Questions { get; set; }

        public CertificateExaminationView()
        {
            Questions = new List<CertificateExaminationQuestionDTO>();
        }
    }
}

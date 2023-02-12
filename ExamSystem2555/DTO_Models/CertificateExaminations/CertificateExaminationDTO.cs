using MyDatabase.Models;
using WebApp.DTO_Models.Certificates;

namespace WebApp.DTO_Models.CertificateExaminations
{
    public class CertificateExaminationDTO
    {
        public int ExaminationId { get; set; }
        public string Status { get; set; }
        public CertificateDTO Certificate { get; set; }
    }
}

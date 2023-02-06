using Microsoft.AspNetCore.Mvc.Rendering;
using MyDatabase.Models;

namespace WebApp.DTO_Models.Final
{
    public class ExaminationDTO
    {
        public int ExaminationId { get; set; }
        public int CertificateId { get; set; }
        public int SelectedCertificateId { get; set; }
        public SelectList? CertificatesList { get; set; }
        public IEnumerable<CertificateExaminationQuestionDTO> Questions { get; set;}
    }
}

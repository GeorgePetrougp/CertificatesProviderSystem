using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyDatabase.Models;
using WebApp.DTO_Models.Certificates;

namespace WebApp.DTO_Models
{
    public class EditQuestionCertificateView
    {
        [ValidateNever]

        public int? QuestionId { get; set; }
        [ValidateNever]

        public List<CertificateDTO> Certificates { get; set; }
        [ValidateNever]

        public MultiSelectList CertificatesList { get; set; }
        [ValidateNever]
        public List<int> SelectedCertificateIds { get; set; }
    }
}

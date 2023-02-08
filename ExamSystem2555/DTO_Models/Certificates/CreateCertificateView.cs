using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO_Models.Certificates
{
    public class CreateCertificateView
    {
        public CertificateDTO CertificateDTO { get; set; }
        [ValidateNever]
        public SelectList CertificateLevelsList { get; set; }
        [Display(Name = "Select Certificate Level")]
        public int SelectedLevelId { get; set; }

    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.DTO_Models.Certificates
{
    public class CreateCertificateView
    {
        public CertificateDTO CertificateDTO { get; set; }
        [ValidateNever]
        public SelectList CertificateLevelsList { get; set; }
        public int SelectedLevelId { get; set; }

        public CreateCertificateView()
        {
            CertificateDTO = new CertificateDTO();
        }
    }
}

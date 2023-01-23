using Microsoft.AspNetCore.Mvc.Rendering;
using MyDatabase.Models;

namespace WebApp.DTO_Models
{
    public class CertificatesView
    {
        public List<int> SelectedCertificateIds { get; set; }
        public SelectList CertificateList { get; set; }
    }
}

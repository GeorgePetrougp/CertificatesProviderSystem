using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using MyDatabase.Models;

namespace WebApp.DTO_Models.Certificates
{
    public class CertificateDTO
    {
        public int CertificateId { get; set; }
        public string Title { get; set; }
        [BindNever]
        [ValidateNever]
        public string FullTitle { get => $"{Title} - {Level.Title}";}
        public string Description { get; set; }
        public string Status { get; set; }
        public int PassMark { get => 65; }
        [BindNever]
        [ValidateNever]
        public CertificateLevelDTO Level { get; set; }

    }
}

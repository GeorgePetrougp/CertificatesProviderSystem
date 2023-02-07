using Microsoft.AspNetCore.Mvc.Rendering;
using MyDatabase.Models;

namespace WebApp.DTO_Models.Certificates
{
    public class CertificateDTO
    {
        public int CertificateId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int PassMark { get => 65; }
        //public Level Level { get; set; }
        
    }
}

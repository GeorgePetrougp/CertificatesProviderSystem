namespace MyDatabase.Models
{
    public class CertificateLevel
    {
        public int CertificateLevelId { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Certificate>? Certificates { get; set;}
        
    }
}
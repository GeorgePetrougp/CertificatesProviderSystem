using System.Collections.Generic;
using System.Net.Security;

namespace MyDatabase.Models
{
    public class Certificate
    {
        public int CertificateId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int PassMark { get { return 65; } private set { } }
        public double Price { get; set; }
        public virtual CertificateLevel Level { get; set; }
        public virtual ICollection<CertificateTopic> CertificateTopics { get; set;}
        public virtual ICollection<Examination> Examinations { get; set;}

    }
}

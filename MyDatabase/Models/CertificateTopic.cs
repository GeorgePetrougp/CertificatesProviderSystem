using System.ComponentModel.DataAnnotations.Schema;

namespace MyDatabase.Models
{
    public class CertificateTopic
    {
        
        public int CertificateTopicId { get; set; }
        public virtual Certificate Certificate { get; set; }
        public virtual Topic? Topic { get; set; }
        //public virtual ICollection<CertificateTopicQuestion> CertificateTopicQuestions { get; set; }
    }
}
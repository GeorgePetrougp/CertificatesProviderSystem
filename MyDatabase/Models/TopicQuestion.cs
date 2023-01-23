using System.ComponentModel.DataAnnotations;

namespace MyDatabase.Models
{
    public class TopicQuestion
    {
        public int TopicQuestionId { get; set; }
        public virtual Topic? Topic { get; set; }
        public virtual Question? Question { get; set; }
        public virtual ICollection<CertificateTopicQuestion>? CertificateTopicQuestions { get; set; }
    }
}
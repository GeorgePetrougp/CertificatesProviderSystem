using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDatabase.Models
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public int MaxScore { get; set; }
        //public virtual ICollection<CertificateTopic> CertificateTopics { get; set; }
        public virtual ICollection<TopicQuestion> TopicQuestions { get; set; }

    }
}

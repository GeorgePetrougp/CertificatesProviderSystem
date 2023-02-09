using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDatabase.Models
{
    public class ExaminationQuestion
    {
        public int ExaminationQuestionId { get; set; }
        public virtual Examination Examination { get; set; }
        public virtual CertificateTopicQuestion CertificateTopicQuestion { get; set; }

    }
}

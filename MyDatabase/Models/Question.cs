using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDatabase.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Display { get; set; }
        public int Points { get; set; }
        public string? Status { get; set; }
        public virtual QuestionDifficulty? QuestionDifficulty { get; set; }
        public virtual ICollection<QuestionPossibleAnswer>? QuestionPossibleAnswers { get; set; }
        public virtual ICollection<TopicQuestion>? TopicQuestions { get; set; }
                                                  
    }
}

using MyDatabase.Models;

namespace WebApp.Models
{
    public class MarkerAssignedExam
    {
        public int MarkerAssignedExamId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual CandidateExam CandidateExam { get; set; }
    }
}

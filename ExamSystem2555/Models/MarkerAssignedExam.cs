using MyDatabase.Models;

namespace WebApp.Models
{
    public class MarkerAssignedExam
    {
        public int MarkerAssignedExamId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Examination Examination { get; set; }
    }
}

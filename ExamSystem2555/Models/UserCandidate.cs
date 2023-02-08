using MyDatabase.Models;

namespace WebApp.Models
{
    public class UserCandidate
    {
        public int UserCandidateId { get; set; }
        public virtual Candidate Candidate { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}

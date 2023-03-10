using MyDatabase.Models;

namespace WebApp.Services.Interfaces
{
    public interface ICandidateService
    {
        Task<Candidate> GetCandidateByIdAsync(int? id);
        Task<IEnumerable<Candidate>> GetAllCandidatesAsync();
        Task<Candidate> AddCandidateAsync(Candidate candidate);
        Task<Candidate> UpdateCandidateAsync(Candidate candidate);
        Task<Candidate> GetCandidateByUserAsync(string userID);
        Task DeleteCandidateAsync(int? id);
    }
}

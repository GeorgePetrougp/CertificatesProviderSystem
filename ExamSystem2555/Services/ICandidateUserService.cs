using MyDatabase.Models;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IUserCandidateService
    {
        Task<UserCandidate> GetUserCandidateByIdAsync(int? id);
        Task<IEnumerable<UserCandidate>> GetAllUserCandidatesAsync();
        Task<UserCandidate> AddUserCandidateAsync(UserCandidate entity);
        Task<UserCandidate> UpdateUserCandidateAsync(UserCandidate entity);
        Task DeleteUserCandidateAsync(int? id);
        string CheckNull(UserCandidate entity);
    }
}

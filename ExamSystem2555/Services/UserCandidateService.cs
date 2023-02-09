using MyDatabase.Models;
using WebApp.Models;
using WebApp.Repositories.Interfaces;
using WebApp.Services.Interfaces;

namespace WebApp.Services
{
    public class UserCandidateService:IUserCandidateService
    {
        private IAsyncGenericRepository<UserCandidate> _UserCandidateRepository;

        public UserCandidateService(IAsyncGenericRepository<UserCandidate> UserCandidateRepository)
        {
            _UserCandidateRepository = UserCandidateRepository;
        }

        public async Task<UserCandidate> GetUserCandidateByIdAsync(int? id)
        {
            return await _UserCandidateRepository.GetByIdAsync(id);
        }


        public async Task<IEnumerable<UserCandidate>> GetAllUserCandidatesAsync()
        {
            return await _UserCandidateRepository.GetAllAsync();
        }

        public async Task<UserCandidate> AddUserCandidateAsync(UserCandidate UserCandidate)
        {
            return await _UserCandidateRepository.AddAsync(UserCandidate);
        }

        public async Task<UserCandidate> UpdateUserCandidateAsync(UserCandidate UserCandidate)
        {
            return await (_UserCandidateRepository.UpdateAsync(UserCandidate));
        }

        public async Task DeleteUserCandidateAsync(int? id)
        {
            await _UserCandidateRepository.DeleteAsync(id);
        }

        public string CheckNull(UserCandidate UserCandidate)
        {
            if (UserCandidate != null)
            {
                return "OK";
            }

            return null;
        }

        
    }
}

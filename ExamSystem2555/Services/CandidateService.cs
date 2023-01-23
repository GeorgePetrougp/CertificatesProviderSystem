using WebApp.Repositories;
using MyDatabase.Models;

namespace WebApp.Services
{
    public class CandidateService:ICandidateService
    {
        private IAsyncGenericRepository<Candidate> _candidateRepository;

        public CandidateService(IAsyncGenericRepository<Candidate> candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<Candidate> GetCandidateByIdAsync(int? id)
        {
            return await _candidateRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Candidate>> GetAllCandidatesAsync()
        {
            return await _candidateRepository.GetAllAsync();
        }

        public async Task<Candidate> AddCandidateAsync(Candidate candidate)
        {
            return await _candidateRepository.AddAsync(candidate);
        }

        public async Task<Candidate> UpdateCandidateAsync(Candidate candidate)
        {
            return await _candidateRepository.UpdateAsync(candidate);
        }

        public async Task DeleteCandidateAsync(int? id)
        {
            await _candidateRepository.DeleteAsync(id);
        }

        public string CheckNull(Topic topic)
        {
            if (topic != null)
            {
                return "OK";
            }

            return null;
        }
    }
}

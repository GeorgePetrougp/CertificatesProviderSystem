using MyDatabase.Models;
using WebApp.Repositories;

namespace WebApp.Services
{
    public class CandidateExamResultsService: ICandidateExamResultsService
    {
        private IAsyncGenericRepository<CandidateExamResults> _candidateExamResultsRepository;

        public CandidateExamResultsService(IAsyncGenericRepository<CandidateExamResults> candidateExamResultsRepository)
        {
            _candidateExamResultsRepository = candidateExamResultsRepository;
        }

        public async Task<CandidateExamResults> GetCandidateExamResultByIdAsync(int? id)
        {
            return await _candidateExamResultsRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<CandidateExamResults>> GetAllCandidateExamResultsAsync()
        {
            return await _candidateExamResultsRepository.GetAllAsync();
        }

        public async Task<CandidateExamResults> AddCandidateExamResultsAsync(CandidateExamResults candidateExamResults)
        {
            return await _candidateExamResultsRepository.AddAsync(candidateExamResults);
        }

        public async Task<CandidateExamResults> UpdateCandidateExamResultsAsync(CandidateExamResults candidateExamResults)
        {
            return await (_candidateExamResultsRepository.UpdateAsync(candidateExamResults));
        }

        public async Task DeleteCandidateExamResultAsync(int? id)
        {
            await _candidateExamResultsRepository.DeleteAsync(id);
        }

        public string CheckNull(CandidateExamResults candidateExamResults)
        {
            if (candidateExamResults != null)
            {
                return "OK";
            }

            return null;
        }
    }
}

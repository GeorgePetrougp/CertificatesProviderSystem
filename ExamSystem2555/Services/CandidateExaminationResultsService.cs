using MyDatabase.Models;
using WebApp.Repositories.Interfaces;
using WebApp.Services.Interfaces;

namespace WebApp.Services
{
    public class CandidateExaminationResultsService: ICandidateExaminationResultsService
    {
        private IAsyncGenericRepository<CandidateExaminationResult> _candidateExamResultsRepository;

        public CandidateExaminationResultsService(IAsyncGenericRepository<CandidateExaminationResult> candidateExamResultsRepository)
        {
            _candidateExamResultsRepository = candidateExamResultsRepository;
        }

        public async Task<CandidateExaminationResult> GetCandidateExamResultByIdAsync(int? id)
        {
            return await _candidateExamResultsRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<CandidateExaminationResult>> GetAllCandidateExamResultsAsync()
        {
            return await _candidateExamResultsRepository.GetAllAsync();
        }

        public async Task<CandidateExaminationResult> AddCandidateExamResultsAsync(CandidateExaminationResult candidateExamResults)
        {
            return await _candidateExamResultsRepository.AddAsync(candidateExamResults);
        }

        public async Task<CandidateExaminationResult> UpdateCandidateExamResultsAsync(CandidateExaminationResult candidateExamResults)
        {
            return await _candidateExamResultsRepository.UpdateAsync(candidateExamResults);
        }

        public async Task DeleteCandidateExamResultAsync(int? id)
        {
            await _candidateExamResultsRepository.DeleteAsync(id);
        }

        public string CheckNull(CandidateExaminationResult candidateExamResults)
        {
            if (candidateExamResults != null)
            {
                return "OK";
            }

            return null;
        }
    }
}

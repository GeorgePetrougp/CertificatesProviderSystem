using WebApp.Repositories;
using MyDatabase.Models;

namespace WebApp.Services
{
    public class CandidateExamService : ICandidateExamService
    {
        private IAsyncGenericRepository<CandidateExam> _candidateExamRepository;

        public CandidateExamService(IAsyncGenericRepository<CandidateExam> candidateExamRepository)
        {
            _candidateExamRepository = candidateExamRepository;
        }

        public async Task<CandidateExam> GetCandidateExamByIdAsync(int? id)
        {
            return await _candidateExamRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<CandidateExam>> GetAllCandidateExamAsync()
        {
            return await _candidateExamRepository.GetAllAsync();
        }

        public async Task<CandidateExam> AddCandidateExamAsync(CandidateExam candidateExam)
        {
            return await _candidateExamRepository.AddAsync(candidateExam);
        }

        public async Task<CandidateExam> UpdateCandidateExamAsync(CandidateExam candidateExam)
        {
            return await (_candidateExamRepository.UpdateAsync(candidateExam));
        }

        public async Task DeleteCandidateExamAsync(int? id)
        {
            await _candidateExamRepository.DeleteAsync(id);
        }

        public string CheckNull(CandidateExam candidateExam)
        {
            if (candidateExam != null)
            {
                return "OK";
            }

            return null;
        }
    }
}

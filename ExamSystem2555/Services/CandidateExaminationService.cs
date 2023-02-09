using MyDatabase.Models;
using WebApp.Services.Interfaces;
using WebApp.Repositories.Interfaces;

namespace WebApp.Services
{
    public class CandidateExaminationService : ICandidateExaminationService
    {
        private IAsyncGenericRepository<CandidateExamination> _candidateExamRepository;

        public CandidateExaminationService(IAsyncGenericRepository<CandidateExamination> candidateExamRepository)
        {
            _candidateExamRepository = candidateExamRepository;
        }

        public async Task<CandidateExamination> GetCandidateExamByIdAsync(int? id)
        {
            return await _candidateExamRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<CandidateExamination>> GetAllCandidateExamAsync()
        {
            return await _candidateExamRepository.GetAllAsync();
        }

        public async Task<CandidateExamination> AddCandidateExamAsync(CandidateExamination candidateExam)
        {
            return await _candidateExamRepository.AddAsync(candidateExam);
        }

        public async Task<CandidateExamination> UpdateCandidateExamAsync(CandidateExamination candidateExam)
        {
            return await (_candidateExamRepository.UpdateAsync(candidateExam));
        }

        public async Task DeleteCandidateExamAsync(int? id)
        {
            await _candidateExamRepository.DeleteAsync(id);
        }

        public string CheckNull(CandidateExamination candidateExam)
        {
            if (candidateExam != null)
            {
                return "OK";
            }

            return null;
        }
    }
}

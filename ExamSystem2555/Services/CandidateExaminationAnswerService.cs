using MyDatabase.Models;
using WebApp.Services.Interfaces;
using WebApp.Repositories.Interfaces;

namespace WebApp.Services
{
    public class CandidateExaminationAnswerService : ICandidateExaminationAnswerService
    {
        private IAsyncGenericRepository<CandidateExaminationAnswer> _examCandidateAnswerRepository;

        public CandidateExaminationAnswerService(IAsyncGenericRepository<CandidateExaminationAnswer> examCandidateAnswerRepository)
        {
            _examCandidateAnswerRepository = examCandidateAnswerRepository;
        }

        public async Task<CandidateExaminationAnswer> GetExamCandidateAnswerByIdAsync(int? id)
        {
            return await _examCandidateAnswerRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<CandidateExaminationAnswer>> GetAllExamCandidateAnswersAsync()
        {
            return await _examCandidateAnswerRepository.GetAllAsync();
        }

        public async Task<CandidateExaminationAnswer> AddExamCandidateAnswerAsync(CandidateExaminationAnswer examCandidateAnswer)
        {
            return await _examCandidateAnswerRepository.AddAsync(examCandidateAnswer);
        }

        public async Task<CandidateExaminationAnswer> UpdateExamCandidateAnswerAsync(CandidateExaminationAnswer examCandidateAnswer)
        {
            return await (_examCandidateAnswerRepository.UpdateAsync(examCandidateAnswer));
        }

        public async Task DeleteExamCandidateAnswerAsync(int? id)
        {
            await _examCandidateAnswerRepository.DeleteAsync(id);
        }

        public string CheckNull(CandidateExaminationAnswer examCandidateAnswer)
        {
            if (examCandidateAnswer != null)
            {
                return "OK";
            }

            return null;
        }
    }
}

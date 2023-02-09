using WebApp.Repositories;
using MyDatabase.Models;

namespace WebApp.Services
{
    public class ExamCandidateAnswerService : IExamCandidateAnswerService
    {
        private IAsyncGenericRepository<CandidateExaminationAnswer> _examCandidateAnswerRepository;

        public ExamCandidateAnswerService(IAsyncGenericRepository<CandidateExaminationAnswer> examCandidateAnswerRepository)
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

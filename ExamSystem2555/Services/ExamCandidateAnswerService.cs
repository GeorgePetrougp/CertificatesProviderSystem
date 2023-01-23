using WebApp.Repositories;
using MyDatabase.Models;

namespace WebApp.Services
{
    public class ExamCandidateAnswerService : IExamCandidateAnswerService
    {
        private IAsyncGenericRepository<ExamCandidateAnswer> _examCandidateAnswerRepository;

        public ExamCandidateAnswerService(IAsyncGenericRepository<ExamCandidateAnswer> examCandidateAnswerRepository)
        {
            _examCandidateAnswerRepository = examCandidateAnswerRepository;
        }

        public async Task<ExamCandidateAnswer> GetExamCandidateAnswerByIdAsync(int? id)
        {
            return await _examCandidateAnswerRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ExamCandidateAnswer>> GetAllExamCandidateAnswersAsync()
        {
            return await _examCandidateAnswerRepository.GetAllAsync();
        }

        public async Task<ExamCandidateAnswer> AddExamCandidateAnswerAsync(ExamCandidateAnswer examCandidateAnswer)
        {
            return await _examCandidateAnswerRepository.AddAsync(examCandidateAnswer);
        }

        public async Task<ExamCandidateAnswer> UpdateExamCandidateAnswerAsync(ExamCandidateAnswer examCandidateAnswer)
        {
            return await (_examCandidateAnswerRepository.UpdateAsync(examCandidateAnswer));
        }

        public async Task DeleteExamCandidateAnswerAsync(int? id)
        {
            await _examCandidateAnswerRepository.DeleteAsync(id);
        }

        public string CheckNull(ExamCandidateAnswer examCandidateAnswer)
        {
            if (examCandidateAnswer != null)
            {
                return "OK";
            }

            return null;
        }
    }
}

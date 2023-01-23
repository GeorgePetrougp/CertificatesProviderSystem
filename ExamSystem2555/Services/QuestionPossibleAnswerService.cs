using WebApp.Repositories;
using MyDatabase.Models;

namespace WebApp.Services
{
    public class QuestionPossibleAnswerService:IQuestionPossibleAnswerService
    {
        private IAsyncGenericRepository<QuestionPossibleAnswer> _possibleAnswerRepository;

        public QuestionPossibleAnswerService(IAsyncGenericRepository<QuestionPossibleAnswer> possibleAnswerRepository)
        {
            _possibleAnswerRepository = possibleAnswerRepository;
        }

        public async Task<QuestionPossibleAnswer> AddAnswerAsync(QuestionPossibleAnswer answer)
        {
            return await _possibleAnswerRepository.AddAsync(answer);

        }

        public async Task DeleteAnswerAsync(int? id)
        {
            await _possibleAnswerRepository.DeleteAsync(id);

        }

        public async Task<IEnumerable<QuestionPossibleAnswer>> GetAllAnswersAsync()
        {
            return await _possibleAnswerRepository.GetAllAsync();

        }

        public async Task<QuestionPossibleAnswer> GetAnswerByIdAsync(int? id)
        {
            return await _possibleAnswerRepository.GetByIdAsync(id);

        }

        public async Task<QuestionPossibleAnswer> UpdateAnswerAsync(QuestionPossibleAnswer answer)
        {
            return await _possibleAnswerRepository.UpdateAsync(answer);

        }

        public async Task<QuestionPossibleAnswer> AddAnswerAsync(Question question, QuestionPossibleAnswer answer)
        {
            answer.Question = question;
            return await _possibleAnswerRepository.AddAsync(answer);
        }

    }
}

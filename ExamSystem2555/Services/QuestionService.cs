using WebApp.Repositories;
using MyDatabase.Models;

namespace WebApp.Services
{
    public class QuestionService:IQuestionService
    {
        private IAsyncGenericRepository<Question> _questionRepository;
        public QuestionService(IAsyncGenericRepository<Question> questionRepository)
        {
            _questionRepository= questionRepository;
        }

        public async Task<Question> GetQuestionByIdAsync(int? id)
        {
            return await _questionRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await _questionRepository.GetAllAsync();
        }

        public async Task<Question> AddQuestionAsync(Question question)
        {
            return await _questionRepository.AddAsync(question);
        }

        public async Task<Question> UpdateQuestionAsync(Question question)
        {
            return await _questionRepository.UpdateAsync(question);
        }

        public async Task DeleteQuestionAsync(int? id)
        {
            await _questionRepository.DeleteAsync(id);
        }

        
    }
}

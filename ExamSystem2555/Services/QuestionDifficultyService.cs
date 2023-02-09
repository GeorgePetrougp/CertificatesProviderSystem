using MyDatabase.Models;
using WebApp.Services.Interfaces;
using WebApp.Repositories.Interfaces;

namespace WebApp.Services
{
    public class QuestionDifficultyService: IQuestionDifficultyService
    {
        IAsyncGenericRepository<QuestionDifficulty> _questionDifficultyRepository;

        public QuestionDifficultyService(IAsyncGenericRepository<QuestionDifficulty> questionDifficulyRepository)
        {
            _questionDifficultyRepository = questionDifficulyRepository;
        }

        public async Task<QuestionDifficulty> AddDifficultyAsync(QuestionDifficulty questionDifficulty)
        {
            return await _questionDifficultyRepository.AddAsync(questionDifficulty);
        }

        public async Task<QuestionDifficulty> UpdateDifficultyAsync(QuestionDifficulty questionDifficulty)
        {
            return await _questionDifficultyRepository.UpdateAsync(questionDifficulty);
        }

        public async Task DeleteDifficultyAsync(int? id)
        {
            await _questionDifficultyRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<QuestionDifficulty>> GetAllDifficultiesAsync()
        {
            return await _questionDifficultyRepository.GetAllAsync();
        }

        public async Task<QuestionDifficulty> GetDifficultyByIdAsync(int? id)
        {
            return await _questionDifficultyRepository.GetByIdAsync(id);
        }
    }
}

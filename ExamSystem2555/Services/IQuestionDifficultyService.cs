using MyDatabase.Models;

namespace WebApp.Services
{
    public interface IQuestionDifficultyService
    {
        Task<QuestionDifficulty> GetDifficultyByIdAsync(int? id);
        Task<IEnumerable<QuestionDifficulty>> GetAllDifficultiesAsync();
        Task<QuestionDifficulty> AddDifficultyAsync(QuestionDifficulty entity);
        Task<QuestionDifficulty> UpdateDifficultyAsync(QuestionDifficulty entity);
        Task DeleteDifficultyAsync(int? id);
    }
}
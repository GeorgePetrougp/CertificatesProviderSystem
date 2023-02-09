using MyDatabase.Models;

namespace WebApp.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<Question> GetQuestionByIdAsync(int? id);
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<Question> AddQuestionAsync(Question entity);
        Task<Question> UpdateQuestionAsync(Question entity);
        Task DeleteQuestionAsync(int? id);

    }
}
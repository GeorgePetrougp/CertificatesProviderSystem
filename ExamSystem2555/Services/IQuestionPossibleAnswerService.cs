using MyDatabase.Models;

namespace WebApp.Services
{
    public interface IQuestionPossibleAnswerService
    {
        Task<QuestionPossibleAnswer> GetAnswerByIdAsync(int? id);
        Task<IEnumerable<QuestionPossibleAnswer>> GetAllAnswersAsync();
        Task<QuestionPossibleAnswer> AddAnswerAsync(QuestionPossibleAnswer answer);
        Task<QuestionPossibleAnswer> UpdateAnswerAsync(QuestionPossibleAnswer answer);
        Task DeleteAnswerAsync(int? id);
        Task<QuestionPossibleAnswer> AddAnswerAsync(Question question, QuestionPossibleAnswer answer);
        Task AddAnswersRange(IEnumerable<QuestionPossibleAnswer> answers);
    }
}

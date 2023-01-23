using MyDatabase.Models;

namespace WebApp.Services
{
    public interface IExaminationQuestionService
    {
        Task<ExaminationQuestion> GetExaminationQuestionByIdAsync(int? id);
        Task<IEnumerable<ExaminationQuestion>> GetAllExaminationQuestionsAsync();
        Task<ExaminationQuestion> AddExaminationQuestionAsync(ExaminationQuestion question);
        Task<ExaminationQuestion> UpdateExaminationQuestionAsync(ExaminationQuestion question);
        Task DeleteExaminationQuestionAsync(int? id);
        string CheckNull(ExaminationQuestion entity);
    }
}

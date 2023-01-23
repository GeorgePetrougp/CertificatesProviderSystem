using MyDatabase.Models;

namespace WebApp.Services
{
    public interface IExamCandidateAnswerService
    {
        Task<ExamCandidateAnswer> GetExamCandidateAnswerByIdAsync(int? id);
        Task<IEnumerable<ExamCandidateAnswer>> GetAllExamCandidateAnswersAsync();
        Task<ExamCandidateAnswer> AddExamCandidateAnswerAsync(ExamCandidateAnswer entity);
        Task<ExamCandidateAnswer> UpdateExamCandidateAnswerAsync(ExamCandidateAnswer entity);
        Task DeleteExamCandidateAnswerAsync(int? id);
        string CheckNull(ExamCandidateAnswer entity);
    }
}

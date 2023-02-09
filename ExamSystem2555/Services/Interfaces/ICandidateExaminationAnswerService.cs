using MyDatabase.Models;

namespace WebApp.Services.Interfaces
{
    public interface ICandidateExaminationAnswerService
    {
        Task<CandidateExaminationAnswer> GetExamCandidateAnswerByIdAsync(int? id);
        Task<IEnumerable<CandidateExaminationAnswer>> GetAllExamCandidateAnswersAsync();
        Task<CandidateExaminationAnswer> AddExamCandidateAnswerAsync(CandidateExaminationAnswer entity);
        Task<CandidateExaminationAnswer> UpdateExamCandidateAnswerAsync(CandidateExaminationAnswer entity);
        Task DeleteExamCandidateAnswerAsync(int? id);
        string CheckNull(CandidateExaminationAnswer entity);
    }
}

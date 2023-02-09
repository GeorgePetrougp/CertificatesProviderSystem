using MyDatabase.Models;

namespace WebApp.Services
{
    public interface ICandidateExamResultsService
    {
        Task<CandidateExaminationResult> GetCandidateExamResultByIdAsync(int? id);
        Task<IEnumerable<CandidateExaminationResult>> GetAllCandidateExamResultsAsync();
        Task<CandidateExaminationResult> AddCandidateExamResultsAsync(CandidateExaminationResult entity);
        Task<CandidateExaminationResult> UpdateCandidateExamResultsAsync(CandidateExaminationResult entity);
        Task DeleteCandidateExamResultAsync(int? id);
        string CheckNull(CandidateExaminationResult entity);
    }
}

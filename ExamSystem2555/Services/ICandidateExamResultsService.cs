using MyDatabase.Models;

namespace WebApp.Services
{
    public interface ICandidateExamResultsService
    {
        Task<CandidateExamResults> GetCandidateExamResultByIdAsync(int? id);
        Task<IEnumerable<CandidateExamResults>> GetAllCandidateExamResultsAsync();
        Task<CandidateExamResults> AddCandidateExamResultsAsync(CandidateExamResults entity);
        Task<CandidateExamResults> UpdateCandidateExamResultsAsync(CandidateExamResults entity);
        Task DeleteCandidateExamResultAsync(int? id);
        string CheckNull(CandidateExamResults entity);
    }
}

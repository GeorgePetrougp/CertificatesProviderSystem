using MyDatabase.Models;

namespace WebApp.Services
{
    public interface ICandidateExamService
    {
        Task<CandidateExam> GetCandidateExamByIdAsync(int? id);
        Task<IEnumerable<CandidateExam>> GetAllCandidateExamAsync();
        Task<CandidateExam> AddCandidateExamAsync(CandidateExam entity);
        Task<CandidateExam> UpdateCandidateExamAsync(CandidateExam entity);
        Task DeleteCandidateExamAsync(int? id);
        string CheckNull(CandidateExam entity);
    }
}

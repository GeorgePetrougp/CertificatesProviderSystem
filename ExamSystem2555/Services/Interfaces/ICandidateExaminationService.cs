using MyDatabase.Models;

namespace WebApp.Services.Interfaces
{
    public interface ICandidateExaminationService
    {
        Task<CandidateExamination> GetCandidateExamByIdAsync(int? id);
        Task<IEnumerable<CandidateExamination>> GetAllCandidateExamAsync();
        Task<CandidateExamination> AddCandidateExamAsync(CandidateExamination entity);
        Task<CandidateExamination> UpdateCandidateExamAsync(CandidateExamination entity);
        Task DeleteCandidateExamAsync(int? id);
        string CheckNull(CandidateExamination entity);
    }
}

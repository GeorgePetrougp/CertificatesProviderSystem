using MyDatabase.Models;

namespace WebApp.Services
{
    public interface IExaminationService
    {
        Task<Examination> GetExaminationByIdAsync(int? id);
        Task<IEnumerable<Examination>> GetAllExaminationsAsync();
        Task<Examination> AddExaminationAsync(Examination examination);
        Task<Examination> UpdateExaminationAsync(Examination examination);
        Task DeleteExaminationAsync(int? id);
    }
}

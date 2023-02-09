using MyDatabase.Models;
using WebApp.Models;

namespace WebApp.Services.Interfaces
{
    public interface IMarkerAssignedExamService
    {
        Task<MarkerAssignedExam> GetMarkerAssignedExamByIdAsync(int? id);
        Task<IEnumerable<MarkerAssignedExam>> GetAllMarkerAssignedExamsAsync();
        Task<MarkerAssignedExam> AddMarkerAssignedExamAsync(MarkerAssignedExam markerAssignedExam);
        Task<MarkerAssignedExam> UpdateMarkerAssignedExamAsync(MarkerAssignedExam markerAssignedExam);
        Task DeleteMarkerAssignedExamAsync(int? id);
        string CheckNull(MarkerAssignedExam markerAssignedExam);
    }
}

using MyDatabase.Models;
using WebApp.Models;
using WebApp.Repositories;

namespace WebApp.Services
{
    public class MarkerAssignedExamService : IMarkerAssignedExamService
    {
        private IAsyncGenericRepository<MarkerAssignedExam> _markerAssignedExamRepository;
        public MarkerAssignedExamService(IAsyncGenericRepository<MarkerAssignedExam> markerAssignedExamRepository)
        {
            _markerAssignedExamRepository = markerAssignedExamRepository;
        }

        public async Task<MarkerAssignedExam> GetMarkerAssignedExamByIdAsync(int? id)
        {
            return await _markerAssignedExamRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<MarkerAssignedExam>> GetAllMarkerAssignedExamsAsync()
        {
            return await _markerAssignedExamRepository.GetAllAsync();
        }

        public async Task<MarkerAssignedExam> AddMarkerAssignedExamAsync(MarkerAssignedExam markerAssignedExam)
        {
            return await _markerAssignedExamRepository.AddAsync(markerAssignedExam);
        }

        public async Task<MarkerAssignedExam> UpdateMarkerAssignedExamAsync(MarkerAssignedExam markerAssignedExam)
        {
            return await _markerAssignedExamRepository.UpdateAsync(markerAssignedExam);
        }

        public async Task DeleteMarkerAssignedExamAsync(int? id)
        {
            await _markerAssignedExamRepository.DeleteAsync(id);
        }

        public string CheckNull(MarkerAssignedExam markerAssignedExam)
        {
            if (markerAssignedExam != null)
            {
                return "OK";
            }

            return null;
        }
    }
}

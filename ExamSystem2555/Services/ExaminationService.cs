using MyDatabase.Models;
using WebApp.Services.Interfaces;
using WebApp.Repositories.Interfaces;

namespace WebApp.Services
{
    public class ExaminationService:IExaminationService
    {
        private IAsyncGenericRepository<Examination> _examinationRepository;

        public ExaminationService(IAsyncGenericRepository<Examination> examinationRepository)
        {
            _examinationRepository = examinationRepository;
        }

        public async Task<Examination> GetExaminationByIdAsync(int? id)
        {
            return await _examinationRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Examination>> GetAllExaminationsAsync()
        {
            return await _examinationRepository.GetAllAsync();
        }

        public async Task<Examination> AddExaminationAsync(Examination examination)
        {
            return await _examinationRepository.AddAsync(examination);
        }

        public async Task<Examination> UpdateExaminationAsync(Examination examination)
        {
            return await _examinationRepository.UpdateAsync(examination);
        }

        public async Task DeleteExaminationAsync(int? id)
        {
            await _examinationRepository.DeleteAsync(id);
        }
    }
}

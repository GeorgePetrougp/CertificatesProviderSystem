using WebApp.Repositories;
using MyDatabase.Models;

namespace WebApp.Services
{
    public class LevelService : ILevelService
    {

        private IAsyncGenericRepository<CertificateLevel> _levelRepository;

        public LevelService(IAsyncGenericRepository<CertificateLevel> levelRepository)
        {
            _levelRepository = levelRepository;
        }

        public async Task<CertificateLevel> GetLevelByIdAsync(int? id)
        {
            return await _levelRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<CertificateLevel>> GetAllLevelsAsync()
        {
            return await _levelRepository.GetAllAsync();
        }

        public async Task<CertificateLevel> AddLevelAsync(CertificateLevel level)
        {
            return await _levelRepository.AddAsync(level);
        }

        public async Task<CertificateLevel> UpdateLevelAsync(CertificateLevel level)
        {
            return await (_levelRepository.UpdateAsync(level));
        }

        public async Task DeleteLevelAsync(int? id)
        {
            await _levelRepository.DeleteAsync(id);
        }

        public string CheckNull(CertificateLevel level)
        {
            if (level != null)
            {
                return "OK";
            }

            return null;
        }
    }  
}

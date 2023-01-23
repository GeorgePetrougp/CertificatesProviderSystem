using WebApp.Repositories;
using MyDatabase.Models;

namespace WebApp.Services
{
    public class LevelService : ILevelService
    {

        private IAsyncGenericRepository<Level> _levelRepository;

        public LevelService(IAsyncGenericRepository<Level> levelRepository)
        {
            _levelRepository = levelRepository;
        }

        public async Task<Level> GetLevelByIdAsync(int? id)
        {
            return await _levelRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Level>> GetAllLevelsAsync()
        {
            return await _levelRepository.GetAllAsync();
        }

        public async Task<Level> AddLevelAsync(Level level)
        {
            return await _levelRepository.AddAsync(level);
        }

        public async Task<Level> UpdateLevelAsync(Level level)
        {
            return await (_levelRepository.UpdateAsync(level));
        }

        public async Task DeleteLevelAsync(int? id)
        {
            await _levelRepository.DeleteAsync(id);
        }

        public string CheckNull(Level level)
        {
            if (level != null)
            {
                return "OK";
            }

            return null;
        }
    }  
}

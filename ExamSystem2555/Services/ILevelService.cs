using MyDatabase.Models;

namespace WebApp.Services
{
    public interface ILevelService
    {
        Task<Level> GetLevelByIdAsync(int? id);
        Task<IEnumerable<Level>> GetAllLevelsAsync();
        Task<Level> AddLevelAsync(Level level);
        Task<Level> UpdateLevelAsync(Level level);
        Task DeleteLevelAsync(int? id);
        string CheckNull(Level entity);
    }
}

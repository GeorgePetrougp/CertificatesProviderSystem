using MyDatabase.Models;

namespace WebApp.Services
{
    public interface ILevelService
    {
        Task<CertificateLevel> GetLevelByIdAsync(int? id);
        Task<IEnumerable<CertificateLevel>> GetAllLevelsAsync();
        Task<CertificateLevel> AddLevelAsync(CertificateLevel level);
        Task<CertificateLevel> UpdateLevelAsync(CertificateLevel level);
        Task DeleteLevelAsync(int? id);
        string CheckNull(CertificateLevel entity);
    }
}

using MyDatabase.Models;

namespace WebApp.Services.Interfaces
{
    public interface ICertificateLevelService
    {
        Task<CertificateLevel> GetLevelByIdAsync(int? id);
        Task<IEnumerable<CertificateLevel>> GetAllLevelsAsync();
        Task<CertificateLevel> AddLevelAsync(CertificateLevel level);
        Task<CertificateLevel> UpdateLevelAsync(CertificateLevel level);
        Task DeleteLevelAsync(int? id);
        string CheckNull(CertificateLevel entity);
    }
}

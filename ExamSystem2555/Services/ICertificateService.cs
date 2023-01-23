using MyDatabase.Models;

namespace WebApp.Services
{
    public interface ICertificateService
    {
        Task<Certificate> GetCertificateByIdAsync(int? id);
        Task<IEnumerable<Certificate>> GetAllCertificatesAsync();
        Task<Certificate> AddCertificateAsync(Certificate certificate);
        Task<Certificate> UpdateCertificateAsync(Certificate certificate);
        Task DeleteCertificateAsync(int? id);
    }
}

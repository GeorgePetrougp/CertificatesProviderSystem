using MyDatabase.Models;

namespace WebApp.Services.Interfaces
{
    public interface ICertificateService
    {
        Task<Certificate> GetCertificateByIdAsync(int? id);
        Task<IEnumerable<Certificate>> GetAllCertificatesAsync();
        Task<Certificate> AddCertificateAsync(Certificate certificate);
        Task<Certificate> UpdateCertificateAsync(Certificate certificate);
        Task<IEnumerable<Certificate>> SortCertificatesById(IEnumerable<int> certificateIds);
        Task DeleteCertificateAsync(int? id);
    }
}

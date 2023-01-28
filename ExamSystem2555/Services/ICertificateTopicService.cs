using MyDatabase.Models;

namespace WebApp.Services
{
    public interface ICertificateTopicService
    {
        Task<CertificateTopic> GetCertificateTopicByIdAsync(int? id);
        Task<IEnumerable<CertificateTopic>> GetAllCertificateTopicsAsync();
        Task<CertificateTopic> AddCertificateTopicAsync(CertificateTopic entity);
        Task<CertificateTopic> UpdateCertificateTopicAsync(CertificateTopic entity);
        Task DeleteCertificateTopicAsync(int? id);
        Task<IEnumerable<CertificateTopic>> SortCTByTopic(Topic topic);

    }
}
using MyDatabase.Models;
using WebApp.Repositories;

namespace WebApp.Services
{
    public class CertificateTopicService:ICertificateTopicService
    {
        private IAsyncGenericRepository<CertificateTopic> _certificateTopicRepository;
        public CertificateTopicService(IAsyncGenericRepository<CertificateTopic> certificateTopicRepository)
        {
            _certificateTopicRepository = certificateTopicRepository;
        }

        public async Task<CertificateTopic> GetCertificateTopicByIdAsync(int? id)
        {
            return await _certificateTopicRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<CertificateTopic>> GetAllCertificateTopicsAsync()
        {
            return await _certificateTopicRepository.GetAllAsync();
        }

        public async Task<CertificateTopic> AddCertificateTopicAsync(CertificateTopic question)
        {
            return await _certificateTopicRepository.AddAsync(question);
        }

        public async Task<CertificateTopic> UpdateCertificateTopicAsync(CertificateTopic question)
        {
            return await _certificateTopicRepository.UpdateAsync(question);
        }

        public async Task DeleteCertificateTopicAsync(int? id)
        {
            await _certificateTopicRepository.DeleteAsync(id);
        }

    }
}

using WebApp.Repositories;
using MyDatabase.Models;

namespace WebApp.Services
{
    public class CertificateService:ICertificateService
    {
        private IAsyncGenericRepository<Certificate> _certificateRepository;

        public CertificateService(IAsyncGenericRepository<Certificate> certificateRepository)
        {
            _certificateRepository = certificateRepository;
        }

        public async Task<Certificate> GetCertificateByIdAsync(int? id)
        {
            return await _certificateRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Certificate>> GetAllCertificatesAsync()
        {
            return await _certificateRepository.GetAllAsync();
        }

        public async Task<Certificate> AddCertificateAsync(Certificate certificate)
        {
            return await _certificateRepository.AddAsync(certificate);
        }

        public async Task<Certificate> UpdateCertificateAsync(Certificate certificate)
        {
            return await _certificateRepository.UpdateAsync(certificate);
        }

        public async Task DeleteCertificateAsync(int? id)
        {
            await _certificateRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Certificate>> SortCertificatesById(IEnumerable<int> certificateIds)
        {
            var sortedCertificates = (await GetAllCertificatesAsync()).Where(ci => certificateIds.Contains(ci.CertificateId)).ToList();
            return sortedCertificates;
        }

        public string CheckNull(Topic topic)
        {
            if (topic != null)
            {
                return "OK";
            }

            return null;
        }
    }
}

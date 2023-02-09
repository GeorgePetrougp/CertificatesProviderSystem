using MyDatabase.Models;
using WebApp.Services.Interfaces;
using WebApp.Repositories.Interfaces;

namespace WebApp.Services
{
    public class CertificateTopicQuestionService : ICertificateTopicQuestionService
    {
        private IAsyncGenericRepository<CertificateTopicQuestion> _certificateTopicQuestionRepository;

        public CertificateTopicQuestionService(IAsyncGenericRepository<CertificateTopicQuestion> certificateTopicQuestionRepository)
        {
            _certificateTopicQuestionRepository = certificateTopicQuestionRepository;
        }

        public async Task<CertificateTopicQuestion> GetCertificateTopicQuestionByIdAsync(int? id)
        {
            return await _certificateTopicQuestionRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<CertificateTopicQuestion>> GetAllCertificateTopicQuestionsAsync()
        {
            return await _certificateTopicQuestionRepository.GetAllAsync();
        }

        public async Task<CertificateTopicQuestion> AddCertificateTopicQuestionAsync(CertificateTopicQuestion certificateTopicQuestion)
        {
            return await _certificateTopicQuestionRepository.AddAsync(certificateTopicQuestion);
        }

        public async Task<CertificateTopicQuestion> UpdateCertificateTopicQuestionAsync(CertificateTopicQuestion certificateTopicQuestion)
        {
            return await (_certificateTopicQuestionRepository.UpdateAsync(certificateTopicQuestion));
        }

        public async Task DeleteCertificateTopicQuestionAsync(int? id)
        {
            await _certificateTopicQuestionRepository.DeleteAsync(id);
        }

        public async Task<CertificateTopicQuestion> AddCertificateTopicQuestionAsync(CertificateTopic certicateTopic, TopicQuestion topicQuestion)
        {
            var newCertificateTopicQuestion = new CertificateTopicQuestion
            {
                CertificateTopic = certicateTopic,
                TopicQuestion = topicQuestion
            };

            return await AddCertificateTopicQuestionAsync(newCertificateTopicQuestion);
        }

        public async Task<IEnumerable<CertificateTopicQuestion>> AddCTQFromList(IEnumerable<CertificateTopicQuestion> certifications)
        {
            await _certificateTopicQuestionRepository.AddRangeAsync(certifications);
            return certifications;
        }

        public async Task<IEnumerable<CertificateTopicQuestion>> AddRangeCTQ()
        {
            return null;
        }

        public string CheckNull(CertificateTopicQuestion certificateTopicQuestion)
        {
            if (certificateTopicQuestion != null)
            {
                return "OK";
            }

            return null;
        }

        public Task<CertificateTopicQuestion> UpdateCertificateTopicQuestionAsync(CertificateTopic erticateTopic, TopicQuestion topicQuestion)
        {

            throw new NotImplementedException();
        }
    }
}

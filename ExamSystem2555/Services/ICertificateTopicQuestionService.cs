using MyDatabase.Models;

namespace WebApp.Services
{
    public interface ICertificateTopicQuestionService
    {
        Task<CertificateTopicQuestion> GetCertificateTopicQuestionByIdAsync(int? id);
        Task<IEnumerable<CertificateTopicQuestion>> GetAllCertificateTopicQuestionsAsync();
        Task<CertificateTopicQuestion> AddCertificateTopicQuestionAsync(CertificateTopicQuestion entity);
        Task<CertificateTopicQuestion> AddCertificateTopicQuestionAsync(CertificateTopic certicateTopic, TopicQuestion topicQuestion);
        Task<CertificateTopicQuestion> UpdateCertificateTopicQuestionAsync(CertificateTopicQuestion entity);
        Task DeleteCertificateTopicQuestionAsync(int? id);
        string CheckNull(CertificateTopicQuestion entity);
    }
}

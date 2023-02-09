using MyDatabase.Models;

namespace WebApp.Services.Interfaces
{
    public interface ICertificateTopicQuestionService
    {
        Task<CertificateTopicQuestion> GetCertificateTopicQuestionByIdAsync(int? id);
        Task<IEnumerable<CertificateTopicQuestion>> GetAllCertificateTopicQuestionsAsync();
        Task<CertificateTopicQuestion> AddCertificateTopicQuestionAsync(CertificateTopicQuestion entity);
        Task<CertificateTopicQuestion> AddCertificateTopicQuestionAsync(CertificateTopic certicateTopic, TopicQuestion topicQuestion);
        Task<CertificateTopicQuestion> UpdateCertificateTopicQuestionAsync(CertificateTopicQuestion entity);
        Task<CertificateTopicQuestion> UpdateCertificateTopicQuestionAsync(CertificateTopic erticateTopic, TopicQuestion topicQuestion);

        Task DeleteCertificateTopicQuestionAsync(int? id);
        string CheckNull(CertificateTopicQuestion entity);
    }
}

using MyDatabase.Models;
using WebApp.Services;

namespace WebApp.MainServices
{
    public interface ICertificateManagerService
    {
        public ICertificateService CertificateService { get; }
        public ICertificateTopicService CertificateTopicService { get; }
        public ILevelService LevelService { get; }
        Task LoadTopics(Certificate certificate);
        Task LoadLevel(Certificate certificate);
        Task SaveChangesAsync();

    }
}

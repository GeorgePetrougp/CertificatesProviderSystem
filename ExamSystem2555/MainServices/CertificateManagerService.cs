using MyDatabase.Models;
using WebApp.Data;
using WebApp.Services;

namespace WebApp.MainServices
{
    public class CertificateManagerService : ICertificateManagerService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICertificateService _certificateService;
        private readonly ICertificateTopicService _certificateTopicService;
        private readonly ILevelService _levelService;

        public CertificateManagerService(ApplicationDbContext context, ICertificateService certificateService, ICertificateTopicService certificateTopicService, ILevelService levelService)
        {
            _context = context;
            _certificateService = certificateService;
            _certificateTopicService = certificateTopicService;
            _levelService = levelService;
        }
        public ICertificateService CertificateService { get => _certificateService; }
        public ICertificateTopicService CertificateTopicService { get => _certificateTopicService; }
        public ILevelService LevelService { get => _levelService; }

        public async Task LoadLevel(Certificate certificate)
        {
            await _context.Entry(certificate).Reference(c=>c.Level).LoadAsync();
        }

        public async Task LoadTopics(Certificate certificate)
        {
            await _context.Entry(certificate).Collection(c=>c.CertificateTopics).LoadAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

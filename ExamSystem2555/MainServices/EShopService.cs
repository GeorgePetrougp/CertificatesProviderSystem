using WebApp.Data;
using WebApp.MainServices.Interfaces;
using WebApp.Services.Interfaces;

namespace WebApp.MainServices
{
    public class EShopService : IEShopService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICandidateService _candidateService;
        private readonly ICertificateService _certificateService;
        private readonly ICandidateExaminationService _candidateExamService;
        private readonly IExaminationService _examinationService;

        public EShopService(ApplicationDbContext context, ICandidateService candidateService, ICertificateService certificateService, ICandidateExaminationService candidateExamService, IExaminationService examinationService)
        {
            _candidateService = candidateService;
            _certificateService = certificateService;
            _candidateExamService = candidateExamService;
            _examinationService = examinationService;
            _context = context;
        }

        public ICandidateService CandidateService { get => _candidateService; }
        public ICertificateService CertificateService { get => _certificateService; }
        public ICandidateExaminationService CandidateExamService { get => _candidateExamService; }
        public IExaminationService ExaminationService { get => _examinationService; }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

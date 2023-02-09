using MyDatabase.Models;
using WebApp.Data;
using WebApp.MainServices.Interfaces;
using WebApp.Services;

namespace WebApp.MainServices
{
    public class CandidateManagerService : ICandidateManagerService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICandidateService _candidateService;
        private readonly ICandidateAddressService _addressService;
        private readonly ICertificateService _certificateService;
        private readonly IExaminationService _examService;
        private readonly ICandidateExamService _candidateExamService;
        private readonly IUserCandidateService _userCandidateService;

        public CandidateManagerService(ApplicationDbContext context, ICandidateService candidateService, ICandidateAddressService addressService, ICertificateService certificateService, IExaminationService examService, ICandidateExamService candidateExamService, IUserCandidateService userCandidateService)
        {
            _context = context;
            _candidateService = candidateService;
            _addressService = addressService;
            _certificateService = certificateService;
            _examService = examService;
            _candidateExamService= candidateExamService;
            _userCandidateService = userCandidateService;
        }
        public ICandidateService CandidateService { get => _candidateService; }
        public ICandidateAddressService AddressService { get => _addressService; }
        public ICertificateService CertificateService { get => _certificateService; }
        public IExaminationService ExaminationService { get => _examService; }
        public ICandidateExamService CandidateExamService { get => _candidateExamService; }
        public IUserCandidateService UserCandidateService { get => _userCandidateService; }


        public async Task LoadCandidateAddress(Candidate candidate)
        {
            await _context.Entry(candidate).Collection(c => c.Addresses).LoadAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

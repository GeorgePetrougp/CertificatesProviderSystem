using MyDatabase.Models;
using WebApp.Data;
using WebApp.Services;

namespace WebApp.MainServices
{
    public class CandidateManagerService : ICandidateManagerService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICandidateService _candidateService;
        private readonly IAddressService _addressService;
        public CandidateManagerService(ApplicationDbContext context, ICandidateService candidateService, IAddressService addressService)
        {
            _context = context;
            _candidateService = candidateService;
            _addressService = addressService;
        }
        public ICandidateService CandidateService { get => _candidateService; }
        public IAddressService AddressService { get => _addressService; }


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

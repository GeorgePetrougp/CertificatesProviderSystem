using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using WebApp.Data;
using WebApp.Services;

namespace WebApp.MainServices
{
    public class AdministratorService : IAdministratorService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICandidateExamService _candidateExamService;
        private readonly IMarkerAssignedExamService _markerAssignedExamService;
        private readonly ICandidateExamResultsService _candidateExamResultsService;

        public AdministratorService(ApplicationDbContext context, ICandidateExamService candidateExamService, IMarkerAssignedExamService markerAssignedExamService, ICandidateExamResultsService candidateExamResultsService)
        {
            _candidateExamService = candidateExamService;
            _markerAssignedExamService = markerAssignedExamService;
            _context = context;
            _candidateExamResultsService = candidateExamResultsService;
        }

        public ICandidateExamService CandidateExamService { get => _candidateExamService; }
        public IMarkerAssignedExamService MarkerAssignedExamService { get => _markerAssignedExamService; }
        public ICandidateExamResultsService CandidateExamResultsService { get => _candidateExamResultsService; }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task CandidateExamLoad(IEnumerable<CandidateExam> candidateExam)
        {
            foreach (var item in candidateExam)
            {

                await _context.Entry(item).Reference(c => c.Candidate).LoadAsync();
                await _context.Entry(item).Reference(c => c.Examination).Query().Include(c => c.Certificate).LoadAsync();
            }
        }

        public async Task CandidateExamLoad(CandidateExam candidateExam)
        {
            await _context.Entry(candidateExam).Reference(c => c.Candidate).LoadAsync();
            await _context.Entry(candidateExam).Reference(c => c.CandidateExamResults).LoadAsync();
            await _context.Entry(candidateExam).Reference(c => c.Examination).Query().Include(c => c.Certificate).LoadAsync();
        }
        public async Task CandidateExamResultsLoad(IEnumerable<CandidateExamResults> candidateExamResults)
        {
            foreach (var item in candidateExamResults)
            {

                await _context.Entry(item).Reference(c => c.CandidateExam).LoadAsync();
            }
        }
    }
}

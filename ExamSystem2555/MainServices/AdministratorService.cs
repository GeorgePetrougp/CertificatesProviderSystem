using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using WebApp.Data;
using WebApp.MainServices.Interfaces;
using WebApp.Services.Interfaces;

namespace WebApp.MainServices
{
    public class AdministratorService : IAdministratorService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICandidateExaminationService _candidateExamService;
        private readonly IMarkerAssignedExamService _markerAssignedExamService;
        private readonly ICandidateExaminationResultsService _candidateExamResultsService;

        public AdministratorService(ApplicationDbContext context, ICandidateExaminationService candidateExamService, IMarkerAssignedExamService markerAssignedExamService, ICandidateExaminationResultsService candidateExamResultsService)
        {
            _candidateExamService = candidateExamService;
            _markerAssignedExamService = markerAssignedExamService;
            _context = context;
            _candidateExamResultsService = candidateExamResultsService;
        }

        public ICandidateExaminationService CandidateExamService { get => _candidateExamService; }
        public IMarkerAssignedExamService MarkerAssignedExamService { get => _markerAssignedExamService; }
        public ICandidateExaminationResultsService CandidateExamResultsService { get => _candidateExamResultsService; }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task CandidateExamLoad(IEnumerable<CandidateExamination> candidateExam)
        {
            foreach (var item in candidateExam)
            {

                await _context.Entry(item).Reference(c => c.Candidate).LoadAsync();
                await _context.Entry(item).Reference(c => c.Examination).Query().Include(c => c.Certificate).LoadAsync();
            }
        }

        public async Task CandidateExamLoad(CandidateExamination candidateExam)
        {
            await _context.Entry(candidateExam).Reference(c => c.Candidate).LoadAsync();
            await _context.Entry(candidateExam).Reference(c => c.CandidateExamResults).LoadAsync();
            await _context.Entry(candidateExam).Reference(c => c.Examination).Query().Include(c => c.Certificate).LoadAsync();
        }
        public async Task CandidateExamResultsLoad(IEnumerable<CandidateExaminationResult> candidateExamResults)
        {
            foreach (var item in candidateExamResults)
            {

                await _context.Entry(item).Reference(c => c.CandidateExam).LoadAsync();
            }
        }
    }
}

using MyDatabase.Models;
using WebApp.Services;

namespace WebApp.MainServices.Interfaces
{
    public interface IAdministratorService
    {
        public ICandidateExamService CandidateExamService { get; }
        public ICandidateExamResultsService CandidateExamResultsService { get; }
        public IMarkerAssignedExamService MarkerAssignedExamService { get; }
        public Task CandidateExamLoad(IEnumerable<CandidateExamination> candidateExam);
        public Task CandidateExamLoad(CandidateExamination candidateExam);
        public Task CandidateExamResultsLoad(IEnumerable<CandidateExaminationResult> candidateExamResults);
        public Task SaveChangesAsync();


    }
}

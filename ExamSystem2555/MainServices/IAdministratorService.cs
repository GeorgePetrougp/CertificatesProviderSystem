using MyDatabase.Models;
using WebApp.Services;

namespace WebApp.MainServices
{
    public interface IAdministratorService
    {
        public ICandidateExamService CandidateExamService { get; }
        public ICandidateExamResultsService CandidateExamResultsService { get; }
        public IMarkerAssignedExamService MarkerAssignedExamService { get; }
        public Task CandidateExamLoad(IEnumerable<CandidateExam> candidateExam);
        public Task CandidateExamLoad(CandidateExam candidateExam);
        public Task CandidateExamResultsLoad(IEnumerable<CandidateExamResults> candidateExamResults);
        public Task SaveChangesAsync();


    }
}

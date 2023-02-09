using MyDatabase.Models;
using WebApp.Services.Interfaces;

namespace WebApp.MainServices.Interfaces
{
    public interface IAdministratorService
    {
        public ICandidateExaminationService CandidateExamService { get; }
        public ICandidateExaminationResultsService CandidateExamResultsService { get; }
        public IMarkerAssignedExamService MarkerAssignedExamService { get; }
        public Task CandidateExamLoad(IEnumerable<CandidateExamination> candidateExam);
        public Task CandidateExamLoad(CandidateExamination candidateExam);
        public Task CandidateExamResultsLoad(IEnumerable<CandidateExaminationResult> candidateExamResults);
        public Task SaveChangesAsync();


    }
}

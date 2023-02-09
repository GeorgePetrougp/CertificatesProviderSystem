using MyDatabase.Models;
using WebApp.Services.Interfaces;

namespace WebApp.MainServices.Interfaces
{
    public interface ICandidateExaminationManagerService
    {

        public ICertificateTopicQuestionService CertificateTopicQuestionService { get; }
        public IQuestionService QuestionService { get; }
        public IQuestionPossibleAnswerService AnswerService { get; }
        public ICandidateExaminationAnswerService CandidateAnswerService { get; }
        public IExaminationService ExaminationService { get; }
        public IExaminationQuestionService ExamQuestionService { get; }
        public ICandidateExaminationService CandidateExamService { get; }
        public ICandidateExaminationResultsService CandidateExamResultsService { get; }
        public IMarkerAssignedExamService MarkerAssignedExamService { get; }
        public Task CandidateAnswerExamLoad(IEnumerable<CandidateExaminationAnswer> examCandidateAnswers);
        Task CertificateTopicsLoad(CertificateTopicQuestion ctq);
        Task ExaminationQuestionLoad(IEnumerable<ExaminationQuestion> eq);
        Task CandidateExaminationLoad(CandidateExamination c);
        Task CandidateExaminationLoad(IEnumerable<CandidateExamination> c);

        Task CertificateTopicsLoad(IEnumerable<CertificateTopicQuestion> ctqList);
        Task CertificateTopicsQuestionLoad(CandidateExaminationAnswer examAnswer);
        Task CandidateResultsLoad(CandidateExamination c);
        Task SaveChangesAsync();


    }
}

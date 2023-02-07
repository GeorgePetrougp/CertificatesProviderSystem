using MyDatabase.Models;
using WebApp.Services;

namespace WebApp.MainServices
{
    public interface IExamManagerService
    {
         
        public ICertificateTopicQuestionService CertificateTopicQuestionService { get; }
        public IQuestionService QuestionService { get; }
        public IQuestionPossibleAnswerService AnswerService { get; }
        public IExamCandidateAnswerService CandidateAnswerService { get; }
        public IExaminationService ExaminationService { get; }
        public IExaminationQuestionService ExamQuestionService { get; }
        public ICandidateExamService CandidateExamService { get; }
        public ICandidateExamResultsService CandidateExamResultsService { get; }
        public IMarkerAssignedExamService MarkerAssignedExamService { get; }
        public Task CandidateAnswerExamLoad(IEnumerable<ExamCandidateAnswer> examCandidateAnswers);
        Task CertificateTopicsLoad(CertificateTopicQuestion ctq);
        Task ExaminationQuestionLoad(IEnumerable<ExaminationQuestion> eq);
        Task CandidateExaminationLoad(CandidateExam c);
        Task CertificateTopicsLoad(IEnumerable<CertificateTopicQuestion> ctqList);
        Task CertificateTopicsQuestionLoad(ExamCandidateAnswer examAnswer);
        Task CandidateResultsLoad(CandidateExam c);
        Task SaveChangesAsync();


    }
}

using MyDatabase.Models;
using WebApp.Services;

namespace WebApp.MainServices.Interfaces
{
    public interface IExaminationManagerService
    {
        public ICandidateExamService CandidateExamService { get; }
        public IExaminationQuestionService ExaminationQuestionService { get; }
        public IExaminationService ExaminationService { get; }
        public IQuestionPossibleAnswerService QuestionPossibleAnswerService { get; }
        public ICertificateTopicQuestionService CertificateTopicQuestionService { get; }
        public IExamCandidateAnswerService ExamCandidateAnswerService { get; }
        public ICandidateExamResultsService CandidateExamResultsService { get; }
        public Task CandidateAnswerExamLoad(IEnumerable<CandidateExaminationAnswer> examCandidateAnswers);
        Task CertificateTopicsLoad(CertificateTopicQuestion ctq);
        Task ExaminationQuestionLoad(IEnumerable<ExaminationQuestion> eq);
        Task CandidateExaminationLoad(CandidateExamination c);
        Task CertificateTopicsLoad(IEnumerable<CertificateTopicQuestion> ctqList);
        Task CertificateTopicsQuestionLoad(CandidateExaminationAnswer examAnswer);
        Task CandidateResultsLoad(CandidateExamination c);
        public Task SaveChangesAsync();

        //public IAnswers CandidateExamService { get; }


    }
}

using MyDatabase.Models;
using WebApp.Services.Interfaces;

namespace WebApp.MainServices.Interfaces
{
    public interface IExaminationManagerService
    {
        public ICandidateExaminationService CandidateExamService { get; }
        public IExaminationQuestionService ExaminationQuestionService { get; }
        public IExaminationService ExaminationService { get; }
        public IQuestionPossibleAnswerService QuestionPossibleAnswerService { get; }
        public ICertificateTopicQuestionService CertificateTopicQuestionService { get; }
        public ICandidateExaminationAnswerService ExamCandidateAnswerService { get; }
        public ICandidateExaminationResultsService CandidateExamResultsService { get; }
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

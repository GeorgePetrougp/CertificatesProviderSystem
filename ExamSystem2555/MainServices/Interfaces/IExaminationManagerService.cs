using MyDatabase.Models;
using WebApp.Services.Interfaces;

namespace WebApp.MainServices.Interfaces
{
    public interface IExaminationManagerService
    {
        public ICandidateExaminationService CandidateExamService { get; }
        public IExaminationQuestionService ExaminationQuestionService { get; }
        public IExaminationService ExaminationService { get; }
        public IQuestionService QuestionService { get; }
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
        Task QuestionPossibleAnswersLoad(Question question);
        public Task SaveChangesAsync();

    }
}

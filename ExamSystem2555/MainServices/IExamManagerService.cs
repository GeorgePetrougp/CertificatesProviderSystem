using WebApp.Services;

namespace WebApp.MainServices
{
    public interface IExamManagerService
    {
         
        public ICertificateTopicQuestionService CertificateTopicQuestionService { get; }
        public IQuestionService QuestionService { get; }
        public IQuestionPossibleAnswerService AnswerService { get; }
        public IExamCandidateAnswerService CandidateAnswerService { get; }
        Task SaveChangesAsync();


    }
}

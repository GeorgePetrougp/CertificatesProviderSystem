using WebApp.Data;
using WebApp.DTO_Models;
using WebApp.Services;
using MyDatabase.Models;

namespace WebApp.MainServices
{
    public interface IQuestionManagerService
    {
        public IQuestionService QuestionService { get; }
        public IQuestionDifficultyService QuestionDifficultyService { get; }
        public ITopicService TopicService { get; }
        public ITopicQuestionService TopicQuestionService { get; }
        public ICertificateTopicQuestionService CertificateTopicQuestionService { get; }
        public ICertificateService CertificateService { get; }
        public IQuestionViewService QuestionViewService { get; }
        public IQuestionPossibleAnswerService AnswerService { get; }
        public Task<Question> CreateFromDTO(QuestionView questionView);
        public Task SaveChanges();
    }
}

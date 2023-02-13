using WebApp.Data;
using WebApp.DTO_Models;
using MyDatabase.Models;
using AutoMapper;
using WebApp.Services.Interfaces;

namespace WebApp.MainServices.Interfaces
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
        public ICertificateTopicService CertificateTopicService { get; }


        public IMapper Mapper { get; }
        public Task CertificateLevelLoad(Certificate certificate);
        public Task CertificateLevelLoad(IEnumerable<Certificate> certificateList);
        public Task<Question> CreateNewQuestion(CreateQuestionView question);
        public Task QuestionLoad(Question question);
        public Task TopicQuestionLoad(TopicQuestion topicQuestion);
        public Task CertificateQuestionLoad(CertificateTopicQuestion certificateTopicQuestion);
        public Task QuestionAnswerLoad(QuestionPossibleAnswer answer);
        public Task CertificateTopicsLoad(CertificateTopicQuestion ctq);
        public Task SaveChanges();
    }
}

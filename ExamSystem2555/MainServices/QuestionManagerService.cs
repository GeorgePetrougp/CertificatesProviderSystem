using WebApp.Data;
using WebApp.DTO_Models;
using WebApp.Repositories;
using WebApp.Services;
using MyDatabase.Models;
using NuGet.Packaging;

namespace WebApp.MainServices
{
    public class QuestionManagerService : IQuestionManagerService
    {
        private IQuestionService _questionService;
        private IQuestionDifficultyService _difficultyService;
        private IQuestionPossibleAnswerService _answerService;
        private ITopicService _topicService;
        private ITopicQuestionService _topicQuestionService;
        private ICertificateTopicQuestionService _certificateTopicQuestionService;
        private ICertificateService _certificateService;
        private IQuestionViewService _questionViewService;

        private readonly ApplicationDbContext _context;

        public QuestionManagerService(IQuestionService questionService, IQuestionDifficultyService questionDifficultyService, ITopicService topicService, ITopicQuestionService topicQuestionService, ICertificateTopicQuestionService certificateTopicQuestionService, ICertificateService certificateService, ApplicationDbContext context, IQuestionPossibleAnswerService answerService, IQuestionViewService questionViewService)
        {
            _questionService = questionService;
            _difficultyService = questionDifficultyService;
            _answerService = answerService;
            _topicService = topicService;
            _topicQuestionService = topicQuestionService;
            _certificateTopicQuestionService = certificateTopicQuestionService;
            _certificateService = certificateService;
            _questionViewService = questionViewService;
            _context = context;

        }

        public IQuestionService QuestionService { get { return _questionService; } }
        public IQuestionDifficultyService QuestionDifficultyService { get { return _difficultyService; } }
        public ITopicService TopicService { get { return _topicService; } }
        public ITopicQuestionService TopicQuestionService { get { return _topicQuestionService; } }
        public ICertificateTopicQuestionService CertificateTopicQuestionService { get { return _certificateTopicQuestionService; } }
        public ICertificateService CertificateService { get { return _certificateService; } }
        public IQuestionViewService QuestionViewService { get { return _questionViewService; } }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }


        public async Task AddAnswers(Question question, List<QuestionPossibleAnswer> answers)
        {
            if (answers.Any(c => c.IsCorrect == true))
            {
                answers.ForEach(a => _answerService.AddAnswerAsync(question, a));
            }
        }

        public async Task<Question> CreateFromDTO(MainQuestionVM question)
        {
            throw new NotImplementedException();
        }
    }
}

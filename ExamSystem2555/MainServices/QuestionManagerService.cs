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

        public async Task<Question> CreateFromDTO(QuestionView question)
        {
            var newQuestion = new Question
            {
                QuestionId = question.QuestionId,
                Display = question.Display,
                QuestionDifficulty = await QuestionDifficultyService.GetDifficultyByIdAsync(question.DifficultiesList.SelectedId)


            };

            var topic = _topicService.GetTopicByIdAsync(question.SelectedTopicsId);


            var questionTopic = new TopicQuestion
            {
                Question = newQuestion,
                Topic = await topic
            };
            await _topicQuestionService.AddTopicQuestionAsync(questionTopic);

            var certificateTopicQuestion = new CertificateTopicQuestion
            {
                Certificate = await _certificateService.GetCertificateByIdAsync(question.SelectedCertificateId),
                TopicQuestion = questionTopic
            };
            await _certificateTopicQuestionService.AddCertificateTopicQuestionAsync(certificateTopicQuestion);

            var possibleAnswersA = new QuestionPossibleAnswer
            {
                Question = newQuestion,
                PossibleAnswer = question.AnswerA.PossibleAnswer,
                IsCorrect = question.AnswerA.IsCorrect
            };
            await _answerService.AddAnswerAsync(possibleAnswersA);

            var possibleAnswersB = new QuestionPossibleAnswer
            {
                Question = newQuestion,
                PossibleAnswer = question.AnswerB.PossibleAnswer,
                IsCorrect = question.AnswerB.IsCorrect
            };
            await _answerService.AddAnswerAsync(possibleAnswersB);

            var possibleAnswersC = new QuestionPossibleAnswer
            {
                Question = newQuestion,
                PossibleAnswer = question.AnswerC.PossibleAnswer,
                IsCorrect = question.AnswerC.IsCorrect
            };
            await _answerService.AddAnswerAsync(possibleAnswersC);

            var possibleAnswersD = new QuestionPossibleAnswer
            {
                Question = newQuestion,
                PossibleAnswer = question.AnswerD.PossibleAnswer,
                IsCorrect = question.AnswerD.IsCorrect
            };
            await _answerService.AddAnswerAsync(possibleAnswersD);


            return newQuestion;
        }

        //public async Task CreateQuestioWithTopicAsync(Question question, List<Topic> topics)
        //{
        //    var selectedTopic = _topicService.GetTopicByIdAsync(TopicId);
        //    await _topicQuestionService.AddTopicQuestionAsync(question, await selectedTopic);
        //    await _questionService.AddQuestionAsync(question);
        //}

    }
}

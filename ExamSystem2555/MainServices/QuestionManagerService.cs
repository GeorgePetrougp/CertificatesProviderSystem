using WebApp.Data;
using WebApp.DTO_Models;
using WebApp.Repositories;
using WebApp.Services;
using MyDatabase.Models;
using NuGet.Packaging;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.AccessControl;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
        private ICertificateTopicService _certficiateTopicService;
        private IMapper _mapper;

        private readonly ApplicationDbContext _context;

        public QuestionManagerService(IQuestionService questionService, IQuestionDifficultyService questionDifficultyService, ITopicService topicService, ITopicQuestionService topicQuestionService, ICertificateTopicQuestionService certificateTopicQuestionService, ICertificateService certificateService, ApplicationDbContext context, IQuestionPossibleAnswerService answerService, IQuestionViewService questionViewService, IMapper mapper, ICertificateTopicService certficiateTopicService)
        {
            _questionService = questionService;
            _difficultyService = questionDifficultyService;
            _answerService = answerService;
            _topicService = topicService;
            _topicQuestionService = topicQuestionService;
            _certificateTopicQuestionService = certificateTopicQuestionService;
            _certificateService = certificateService;
            _questionViewService = questionViewService;
            _certficiateTopicService = certficiateTopicService;
            _mapper = mapper;
            _context = context;
        }

        public IQuestionService QuestionService { get { return _questionService; } }
        public IQuestionDifficultyService QuestionDifficultyService { get { return _difficultyService; } }
        public ITopicService TopicService { get { return _topicService; } }
        public ICertificateTopicService CertificateTopicService { get { return _certficiateTopicService; } }
        public ITopicQuestionService TopicQuestionService { get { return _topicQuestionService; } }
        public ICertificateTopicQuestionService CertificateTopicQuestionService { get { return _certificateTopicQuestionService; } }
        public ICertificateService CertificateService { get { return _certificateService; } }
        public IQuestionViewService QuestionViewService { get { return _questionViewService; } }
        public IQuestionPossibleAnswerService AnswerService { get { return _answerService; } }
        public IMapper Mapper { get { return _mapper; } }


        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }


        public async Task<Question> CreateNewQuestion(QuestionView question)
        {
            var newQuestion = _mapper.Map<Question>(question);

            //Adding Question and QuestionDifficulty
            await CreateQuestionWithDifficultyAndAnswers(question, newQuestion);

            //Adding Topics,Certificates
            newQuestion.TopicQuestions = new List<TopicQuestion>();

            if (question.TopicView.SelectedTopicIds == null)
            {
                TopicQuestion myTopicQuestion = await AddQuestionWithNoTopic(newQuestion);
                await AddToCertificateTopicQuestion(question, myTopicQuestion);
            }
            else
            {
                await AddQuestionWithTopic(question, newQuestion);
            }

            await this.SaveChanges();
            return newQuestion;

        }



        private async Task AddToCertificateTopicQuestion(QuestionView question, TopicQuestion myTopicQuestion)
        {
            var certificateIds = question.CertificatesView.SelectedCertificateIds.ToList();

            //await Task.WhenAll(certificateIds.Select(async certId => await CertificateTopicQuestionService.AddCertificateTopicQuestionAsync(new CertificateTopicQuestion
            //{
            //    Certificate = await CertificateService.GetCertificateByIdAsync(certId),
            //    TopicQuestion = myTopicQuestion
            //})));
            foreach (var certId in certificateIds)
            {
                var myCertTopicQuestion = new CertificateTopicQuestion
                {
                    CertificateTopic = null,
                    TopicQuestion = myTopicQuestion

                };
                await CertificateTopicQuestionService.AddCertificateTopicQuestionAsync(myCertTopicQuestion);
            }
        }

        private async Task AddQuestionWithTopic(QuestionView question, Question newQuestion)
        {
            var topicIds = question.TopicView.SelectedTopicIds.ToList();

            foreach (var topicId in topicIds)
            {
                var myTopicQuestion = new TopicQuestion
                {
                    Question = newQuestion,
                    Topic = await TopicService.GetTopicByIdAsync(topicId)
                };
                await TopicQuestionService.AddTopicQuestionAsync(myTopicQuestion);
            }
        }

        private async Task<TopicQuestion> AddQuestionWithNoTopic(Question newQuestion)
        {
            var myTopicQuestion = new TopicQuestion
            {
                Question = newQuestion,
                Topic = null
            };
            await TopicQuestionService.AddTopicQuestionAsync(myTopicQuestion);
            return myTopicQuestion;
        }

        private async Task CreateQuestionWithDifficultyAndAnswers(QuestionView question, Question newQuestion)
        {
            newQuestion.QuestionDifficulty = await QuestionDifficultyService.GetDifficultyByIdAsync(question.Difficulty.SelectedId);
            newQuestion.QuestionPossibleAnswers = _mapper.Map<List<QuestionPossibleAnswer>>(question.AnswerViews);
            await QuestionService.AddQuestionAsync(newQuestion);
        }


        public async Task QuestionLoad(Question question)
        {

            await _context.Entry(question).Reference(q => q.QuestionDifficulty).LoadAsync();
        }

        public async Task QuestionAnswerLoad(QuestionPossibleAnswer answer)
        {

             await _context.Entry(answer).Reference(q => q.Question).LoadAsync();
        }

        public async Task TopicQuestionLoad(TopicQuestion topicQuestion)
        {
            await _context.Entry(topicQuestion).Reference(t=>t.Question).LoadAsync();
            await _context.Entry(topicQuestion).Reference(t=>t.Topic).LoadAsync();

        }
        
        public async Task CertificateTopicsLoad(CertificateTopicQuestion ctq)
        {
            await _context.Entry(ctq).Reference(c => c.CertificateTopic).Query().Include(cert=>cert.Certificate).LoadAsync();
            await _context.Entry(ctq).Reference(c => c.TopicQuestion).Query().Include(cert => cert.Question).LoadAsync();


        }



        public Task CertificateQuestionLoad(CertificateTopicQuestion certificateTopicQuestion)
        {
            throw new NotImplementedException();
        }
    }
}

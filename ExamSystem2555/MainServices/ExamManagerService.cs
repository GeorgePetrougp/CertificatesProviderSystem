using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using WebApp.Data;
using WebApp.Services;

namespace WebApp.MainServices
{
    public class ExamManagerService : IExamManagerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IQuestionService _questionService;
        private readonly IQuestionPossibleAnswerService _answerService;
        private readonly ITopicQuestionService _topicQuestionService;
        private readonly IExamCandidateAnswerService _candidateAnswerService;
        private readonly IExaminationQuestionService _examQuestionService;
        private readonly IExaminationService _examService;
        private readonly ICertificateTopicQuestionService _certificateTopicQuestionService;

        public ExamManagerService(ApplicationDbContext context, ICertificateTopicQuestionService certificateTopicQuestionService , IQuestionService questionService, IQuestionPossibleAnswerService answerService, IExamCandidateAnswerService candidateAnswerService, IExaminationQuestionService examQuestionService, IExaminationService examService, ITopicQuestionService topicQuestionService)
        {
            _context = context;
            _questionService = questionService;
            _answerService = answerService;
            _candidateAnswerService = candidateAnswerService;
            _examQuestionService = examQuestionService;
            _examService = examService;
            _topicQuestionService = topicQuestionService;
            _certificateTopicQuestionService = certificateTopicQuestionService;
        }

        public IQuestionService QuestionService { get { return _questionService; } }
        public IQuestionPossibleAnswerService AnswerService { get { return _answerService; } }
        public IExaminationService ExaminationService { get { return _examService; } }
        public IExamCandidateAnswerService CandidateAnswerService { get { return _candidateAnswerService; } }
        public IExaminationQuestionService ExamQuestionService { get { return _examQuestionService; } }
        public ICertificateTopicQuestionService CertificateTopicQuestionService { get { return _certificateTopicQuestionService; } }

        public ITopicQuestionService TopicQuestionService { get { return _topicQuestionService; } }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task ExaminationQuestionLoad(IEnumerable<ExaminationQuestion>  eq)
        {
            foreach (var item in eq)
            {
            await _context.Entry(item).Reference(c => c.CertificateTopicQuestion).Query().Include(cert => cert.TopicQuestion).ThenInclude(y=>y.Question).LoadAsync();


            }
        }
    }
}

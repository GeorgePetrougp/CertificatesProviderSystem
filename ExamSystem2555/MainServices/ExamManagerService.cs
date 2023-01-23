using WebApp.Data;
using WebApp.Services;

namespace WebApp.MainServices
{
    public class ExamManagerService:IExamManagerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IQuestionService _questionService;
        private readonly IQuestionPossibleAnswerService _answerService;
        private readonly IExamCandidateAnswerService _candidateAnswerService;


        public ExamManagerService(ApplicationDbContext context, IQuestionService questionService, IQuestionPossibleAnswerService answerService, IExamCandidateAnswerService candidateAnswerService)
        {
            _context = context;
            _questionService = questionService;
            _answerService = answerService;
            _candidateAnswerService = candidateAnswerService;
        }

        public IQuestionService QuestionService { get { return _questionService; } }
        public IQuestionPossibleAnswerService AnswerService { get { return _answerService; } }
        public IExamCandidateAnswerService CandidateAnswerService { get { return _candidateAnswerService; } }

        public ICertificateTopicQuestionService CertificateTopicQuestionService => throw new NotImplementedException();

        public async  Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

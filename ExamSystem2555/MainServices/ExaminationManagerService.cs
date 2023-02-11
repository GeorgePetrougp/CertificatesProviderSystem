using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using WebApp.Data;
using WebApp.MainServices.Interfaces;
using WebApp.Services.Interfaces;

namespace WebApp.MainServices
{
    public class ExaminationManagerService : IExaminationManagerService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICandidateExaminationService _candidateExamService;
        private readonly IExaminationQuestionService _examinationQuestionService;
        private readonly IExaminationService _examinationService;
        private readonly IQuestionPossibleAnswerService _questionPossibleAnswerService;
        private readonly ICertificateTopicQuestionService _certificateTopicQuestionService;
        private readonly ICandidateExaminationAnswerService _examCandidateAnswerService;
        private readonly ICandidateExaminationResultsService _candidateExamResultsService;

        public ExaminationManagerService(ApplicationDbContext context, ICandidateExaminationService candidateExamService, IExaminationQuestionService examinationQuestionService, IExaminationService examinationService, IQuestionPossibleAnswerService questionPossibleAnswerService, ICertificateTopicQuestionService certificateTopicQuestionService, ICandidateExaminationAnswerService examCandidateAnswerService, ICandidateExaminationResultsService candidateExamResultsService)
        {
            _context = context;
            _candidateExamService = candidateExamService;
            _examinationQuestionService = examinationQuestionService;
            _examinationService = examinationService;
            _questionPossibleAnswerService = questionPossibleAnswerService;
            _certificateTopicQuestionService = certificateTopicQuestionService;
            _examCandidateAnswerService = examCandidateAnswerService;
            _candidateExamResultsService = candidateExamResultsService;
        }

        public ICandidateExaminationService CandidateExamService { get { return _candidateExamService; } }
        public IExaminationQuestionService ExaminationQuestionService { get { return _examinationQuestionService; } }
        public IExaminationService ExaminationService { get { return _examinationService; } }
        public IQuestionPossibleAnswerService QuestionPossibleAnswerService { get { return _questionPossibleAnswerService; } }
        public ICertificateTopicQuestionService CertificateTopicQuestionService { get { return _certificateTopicQuestionService; } }
        public ICandidateExaminationAnswerService ExamCandidateAnswerService { get { return _examCandidateAnswerService; } }
        public ICandidateExaminationResultsService CandidateExamResultsService { get { return _candidateExamResultsService; } }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task ExaminationQuestionLoad(IEnumerable<ExaminationQuestion> eq)
        {
            foreach (var item in eq)
            {
                await _context.Entry(item).Reference(c => c.CertificateTopicQuestion).Query().Include(cert => cert.TopicQuestion).ThenInclude(y => y.Question).ThenInclude(q=>q.QuestionPossibleAnswers).LoadAsync();

            }
        }

        public async Task CertificateTopicsLoad(CertificateTopicQuestion ctq)
        {
            await _context.Entry(ctq).Reference(c => c.CertificateTopic).Query().Include(cert => cert.Certificate).LoadAsync();
            await _context.Entry(ctq).Reference(c => c.TopicQuestion).Query().Include(cert => cert.Question).LoadAsync();


        }

        public async Task CertificateTopicsQuestionLoad(CandidateExaminationAnswer examAnswer)
        {
            await _context.Entry(examAnswer).Reference(c => c.CertificateTopicQuestion).Query().Include(cert => cert.TopicQuestion).ThenInclude(tq => tq.Question).ThenInclude(q => q.QuestionPossibleAnswers).LoadAsync();

        }

        public async Task CertificateTopicsLoad(IEnumerable<CertificateTopicQuestion> ctqList)
        {
            foreach (var item in ctqList)
            {

                await _context.Entry(item).Reference(c => c.CertificateTopic).Query().Include(cert => cert.Certificate).LoadAsync();
                await _context.Entry(item).Reference(c => c.TopicQuestion).Query().Include(cert => cert.Question).LoadAsync();
            }
        }

        public async Task CandidateAnswerExamLoad(IEnumerable<CandidateExaminationAnswer> examCandidateAnswers)
        {
            foreach (var item in examCandidateAnswers)
            {

                await _context.Entry(item).Reference(c => c.CandidateExam).LoadAsync();
            }
        }

        public async Task CandidateExaminationLoad(CandidateExamination c)
        {
            await _context.Entry(c).Reference(e => e.Examination).Query().Include(x=>x.Certificate).LoadAsync();
            await _context.Entry(c).Reference(c => c.Candidate).LoadAsync();

        }

        public async Task CandidateResultsLoad(CandidateExamination c)
        {
            await _context.Entry(c).Reference(e => e.CandidateExamResults).LoadAsync();
            await _context.Entry(c).Reference(e => e.Candidate).LoadAsync();

        }
    }
}

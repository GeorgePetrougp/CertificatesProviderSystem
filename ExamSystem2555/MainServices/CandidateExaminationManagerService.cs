using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using WebApp.Data;
using WebApp.MainServices.Interfaces;
using WebApp.Services.Interfaces;

namespace WebApp.MainServices
{
    public class CandidateExaminationManagerService : ICandidateExaminationManagerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IQuestionService _questionService;
        private readonly IQuestionPossibleAnswerService _answerService;
        private readonly ITopicQuestionService _topicQuestionService;
        private readonly ICandidateExaminationAnswerService _candidateAnswerService;
        private readonly IExaminationQuestionService _examQuestionService;
        private readonly IExaminationService _examService;
        private readonly ICertificateTopicQuestionService _certificateTopicQuestionService;
        private readonly ICandidateExaminationService _candidateExamService;
        private readonly ICandidateExaminationResultsService _candidateExamResultsService;
        private readonly IMarkerAssignedExamService _markerAssignedExamService;



        public CandidateExaminationManagerService(ApplicationDbContext context, ICandidateExaminationResultsService candidateExamResultsService, ICandidateExaminationService candidateExamService, ICertificateTopicQuestionService certificateTopicQuestionService, IQuestionService questionService, IQuestionPossibleAnswerService answerService, ICandidateExaminationAnswerService candidateAnswerService, IExaminationQuestionService examQuestionService, IExaminationService examService, ITopicQuestionService topicQuestionService, IMarkerAssignedExamService markerAssignedExamService)
        {
            _context = context;
            _questionService = questionService;
            _answerService = answerService;
            _candidateAnswerService = candidateAnswerService;
            _examQuestionService = examQuestionService;
            _examService = examService;
            _topicQuestionService = topicQuestionService;
            _certificateTopicQuestionService = certificateTopicQuestionService;
            _candidateExamService = candidateExamService;
            _candidateExamResultsService = candidateExamResultsService;
            _markerAssignedExamService = markerAssignedExamService;
        }

        public IQuestionService QuestionService { get { return _questionService; } }
        public IQuestionPossibleAnswerService AnswerService { get { return _answerService; } }
        public IExaminationService ExaminationService { get { return _examService; } }
        public ICandidateExaminationAnswerService CandidateAnswerService { get { return _candidateAnswerService; } }
        public IExaminationQuestionService ExamQuestionService { get { return _examQuestionService; } }
        public ICertificateTopicQuestionService CertificateTopicQuestionService { get { return _certificateTopicQuestionService; } }
        public ITopicQuestionService TopicQuestionService { get { return _topicQuestionService; } }
        public ICandidateExaminationService CandidateExamService { get { return _candidateExamService; } }
        public ICandidateExaminationResultsService CandidateExamResultsService { get { return _candidateExamResultsService; } }
        public IMarkerAssignedExamService MarkerAssignedExamService { get { return _markerAssignedExamService; } }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task ExaminationQuestionLoad(IEnumerable<ExaminationQuestion> eq)
        {
            foreach (var item in eq)
            {
                await _context.Entry(item).Reference(c => c.CertificateTopicQuestion).Query().Include(cert => cert.TopicQuestion).ThenInclude(y => y.Question).LoadAsync();


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
                await _context.Entry(item).Collection(c => c.ExamCandidateAnswers).LoadAsync();
            }
        }

        public async Task CandidateAnswerExamLoad(IEnumerable<CandidateExaminationAnswer> examCandidateAnswers)
        {
            foreach (var item in examCandidateAnswers)
            {

                await _context.Entry(item).Reference(c => c.CandidateExam).LoadAsync();
            }
        }

        //public async Task CandidateExaminationLoad(CandidateExamination c)
        //{
        //    await _context.Entry(c).Reference(e=>e.Examination).LoadAsync();
        //}

        public async Task CandidateResultsLoad(CandidateExamination c)
        {
            await _context.Entry(c).Reference(e => e.CandidateExamResults).LoadAsync();
            await _context.Entry(c).Reference(e => e.Candidate).LoadAsync();

        }

        public async Task CandidateExaminationLoad(IEnumerable<CandidateExamination> candidateExam)
        {
            foreach (var item in candidateExam)
            {

                await _context.Entry(item).Reference(c => c.Candidate).LoadAsync();
                await _context.Entry(item).Reference(c => c.Examination).Query().Include(c => c.Certificate).LoadAsync();
            }
        }

        public async Task CandidateExaminationLoad(CandidateExamination candidateExam)
        {
            await _context.Entry(candidateExam).Reference(c => c.Candidate).LoadAsync();
            await _context.Entry(candidateExam).Collection(c => c.ExamCandidateAnswers).LoadAsync();
            await _context.Entry(candidateExam).Reference(c => c.CandidateExamResults).LoadAsync();
            await _context.Entry(candidateExam).Reference(c => c.Examination).Query().Include(c => c.Certificate).LoadAsync();
        }



    }
}

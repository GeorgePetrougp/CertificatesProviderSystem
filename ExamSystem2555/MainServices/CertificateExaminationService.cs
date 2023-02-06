using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using WebApp.Data;
using WebApp.Services;

namespace WebApp.MainServices
{
    public class CertificateExaminationService : ICertificateExaminationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICertificateService _certificateService;
        private readonly IExaminationService _examinationService;
        private readonly IExaminationQuestionService _examinationQuestionService;
        private readonly ICertificateTopicQuestionService _certificateTopicQuestionService;
        private readonly ITopicQuestionService _topicQuestionService;
        private readonly ICertificateTopicService _certificateTopicService;
        private readonly IMarkerAssignedExamService _markerAssignedExamService;

        private readonly IQuestionService _questionService;


        public CertificateExaminationService(ApplicationDbContext context, ICertificateService certificateService, IExaminationService examination, IExaminationQuestionService examinationQuestionService, ICertificateTopicQuestionService certificateTopicQuestionService, ITopicQuestionService topicQuestionService, ICertificateTopicService certificateTopicService, IQuestionService questionService, IMarkerAssignedExamService markerAssignedExamService)
        {
            _certificateService = certificateService;
            _examinationService = examination;
            _examinationQuestionService = examinationQuestionService;
            _certificateTopicQuestionService = certificateTopicQuestionService;
            _topicQuestionService = topicQuestionService;
            _certificateTopicService = certificateTopicService;
            _questionService = questionService;
            _context = context;
            _markerAssignedExamService = markerAssignedExamService;
        }

        public IExaminationQuestionService ExaminationQuestionService { get => _examinationQuestionService; }

        public IExaminationService ExaminationService { get => _examinationService; }

        public ICertificateService CertificateService { get => _certificateService; }

        public ITopicQuestionService TopicQuestionService { get => _topicQuestionService; }

        public ICertificateTopicService CertificateTopicService { get => _certificateTopicService; }

        public ICertificateTopicQuestionService CertificateTopicQuestionService { get => _certificateTopicQuestionService; }

        public IQuestionService QuestionService { get => _questionService; }
        public IMarkerAssignedExamService MarkerAssignedExamService { get => _markerAssignedExamService; }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task LoadCertificates(Examination exam)
        {
            await _context.Entry(exam).Reference(e => e.Certificate).LoadAsync();
        }
        public async Task LoadExamQuestions(Examination exam)
        {
            await _context.Entry(exam).Collection(e => e.ExamQuestions).LoadAsync();
        }

        public async Task LoadCTQ(ExaminationQuestion examQuestion)
        {
            await _context.Entry(examQuestion).Reference(e => e.CertificateTopicQuestion).Query().Include(ct => ct.TopicQuestion).ThenInclude(tq => tq.Question).LoadAsync();
        }

        public async Task CertificateTopicQuestionLoad(CertificateTopicQuestion ctq)
        {
            await _context.Entry(ctq).Reference(c => c.TopicQuestion).Query().Include(cert => cert.Question).LoadAsync();
            await _context.Entry(ctq).Reference(c => c.CertificateTopic).Query().Include(cert => cert.Topic).LoadAsync();

        }

    }
}

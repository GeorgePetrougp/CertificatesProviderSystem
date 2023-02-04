﻿using Microsoft.EntityFrameworkCore;
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
        private readonly ICandidateExamService _candidateExamService;
        private readonly ICandidateExamResultsService _candidateExamResultsService;



        public ExamManagerService(ApplicationDbContext context, ICandidateExamResultsService candidateExamResultsService, ICandidateExamService candidateExamService , ICertificateTopicQuestionService certificateTopicQuestionService , IQuestionService questionService, IQuestionPossibleAnswerService answerService, IExamCandidateAnswerService candidateAnswerService, IExaminationQuestionService examQuestionService, IExaminationService examService, ITopicQuestionService topicQuestionService)
        {
            _context = context;
            _questionService = questionService;
            _answerService = answerService;
            _candidateAnswerService = candidateAnswerService;
            _examQuestionService = examQuestionService;
            _examService = examService;
            _topicQuestionService = topicQuestionService;
            _certificateTopicQuestionService = certificateTopicQuestionService;
            _candidateExamService= candidateExamService;
            _candidateExamResultsService= candidateExamResultsService;
        }

        public IQuestionService QuestionService { get { return _questionService; } }
        public IQuestionPossibleAnswerService AnswerService { get { return _answerService; } }
        public IExaminationService ExaminationService { get { return _examService; } }
        public IExamCandidateAnswerService CandidateAnswerService { get { return _candidateAnswerService; } }
        public IExaminationQuestionService ExamQuestionService { get { return _examQuestionService; } }
        public ICertificateTopicQuestionService CertificateTopicQuestionService { get { return _certificateTopicQuestionService; } }

        public ITopicQuestionService TopicQuestionService { get { return _topicQuestionService; } }
        public ICandidateExamService CandidateExamService { get { return _candidateExamService;} }
        public ICandidateExamResultsService CandidateExamResultsService { get { return _candidateExamResultsService; } }


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

        public async Task CertificateTopicsLoad(CertificateTopicQuestion ctq)
        {
            await _context.Entry(ctq).Reference(c => c.CertificateTopic).Query().Include(cert => cert.Certificate).LoadAsync();
            await _context.Entry(ctq).Reference(c => c.TopicQuestion).Query().Include(cert => cert.Question).LoadAsync();


        }

        public async Task CertificateTopicsQuestionLoad(ExamCandidateAnswer examAnswer)
        {
            await _context.Entry(examAnswer).Reference(c => c.CertificateTopicQuestion).Query().Include(cert => cert.TopicQuestion).ThenInclude(tq => tq.Question).ThenInclude(q=>q.QuestionPossibleAnswers).LoadAsync();



        }

        public async Task CertificateTopicsLoad(IEnumerable<CertificateTopicQuestion> ctqList)
        {
            foreach (var item in ctqList)
            {

            await _context.Entry(item).Reference(c => c.CertificateTopic).Query().Include(cert => cert.Certificate).LoadAsync();
            await _context.Entry(item).Reference(c => c.TopicQuestion).Query().Include(cert => cert.Question).LoadAsync();
            }
        }

        public async Task CandidateAnswerExamLoad(IEnumerable<ExamCandidateAnswer> examCandidateAnswers)
        {
            foreach (var item in examCandidateAnswers)
            {

                await _context.Entry(item).Reference(c => c.CandidateExam).LoadAsync();
            }
        }

        public async Task CandidateExaminationLoad(CandidateExam c)
        {
            await _context.Entry(c).Reference(e=>e.Examination).LoadAsync();
        }

      
    }
}

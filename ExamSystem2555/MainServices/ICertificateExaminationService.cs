﻿using MyDatabase.Models;
using WebApp.Services;

namespace WebApp.MainServices
{
    public interface ICertificateExaminationService
    {
        public IExaminationQuestionService ExaminationQuestionService { get; }
        public IExaminationService ExaminationService { get; }
        public ICertificateService CertificateService { get; }
        public ICertificateTopicQuestionService CertificateTopicQuestionService { get; }
        public ITopicQuestionService TopicQuestionService { get; }
        public ICertificateTopicService CertificateTopicService { get; }
        public IQuestionService QuestionService { get; }
        Task LoadCertificates(Examination exam);
        Task CertificateTopicQuestionLoad(CertificateTopicQuestion ctq);
        Task LoadCTQ(ExaminationQuestion examQuestion);
        Task SaveChanges();
    }
}
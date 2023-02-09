using MyDatabase.Models;
using WebApp.Services;

namespace WebApp.MainServices.Interfaces
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
        public IMarkerAssignedExamService MarkerAssignedExamService { get; }
        Task LoadCertificates(Examination exam);
        Task CertificateTopicQuestionLoad(CertificateTopicQuestion ctq);
        Task LoadCTQ(ExaminationQuestion examQuestion);
        Task LoadExamQuestions(Examination exam);
        Task SaveChanges();
    }
}

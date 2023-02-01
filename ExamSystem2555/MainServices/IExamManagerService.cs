using MyDatabase.Models;
using WebApp.Services;

namespace WebApp.MainServices
{
    public interface IExamManagerService
    {
         
        public ICertificateTopicQuestionService CertificateTopicQuestionService { get; }
        public IQuestionService QuestionService { get; }
        public IQuestionPossibleAnswerService AnswerService { get; }
        public IExamCandidateAnswerService CandidateAnswerService { get; }
        public IExaminationService ExaminationService { get; }
        public IExaminationQuestionService ExamQuestionService { get; }
        Task ExaminationQuestionLoad(IEnumerable<ExaminationQuestion> eq);
        Task SaveChangesAsync();


    }
}

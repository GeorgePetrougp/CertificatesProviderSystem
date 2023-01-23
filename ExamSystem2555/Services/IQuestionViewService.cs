using WebApp.DTO_Models;
using MyDatabase.Models;

namespace WebApp.Services
{
    public interface IQuestionViewService
    {
        public QuestionView CreateQuestion(IEnumerable<QuestionDifficulty> difficulties, IEnumerable<Topic> topics,IEnumerable<Certificate> certificates);
     
    }
}

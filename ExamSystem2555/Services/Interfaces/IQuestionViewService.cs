using WebApp.DTO_Models;
using MyDatabase.Models;

namespace WebApp.Services.Interfaces
{
    public interface IQuestionViewService
    {
        public CreateQuestionView CreateQuestion(IEnumerable<QuestionDifficulty> difficulties, IEnumerable<Topic> topics, IEnumerable<Certificate> certificates);

    }
}

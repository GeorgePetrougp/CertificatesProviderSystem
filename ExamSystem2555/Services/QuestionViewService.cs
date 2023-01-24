using WebApp.DTO_Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyDatabase.Models;

namespace WebApp.Services
{
    public class QuestionViewService : IQuestionViewService
    {
        public QuestionView CreateQuestion(IEnumerable<QuestionDifficulty> difficulties, IEnumerable<Topic> topics,IEnumerable<Certificate> certificates)
        {
            var newQV = new QuestionView();
            return newQV;
        }
    }
}

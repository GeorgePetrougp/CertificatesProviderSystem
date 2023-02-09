using WebApp.DTO_Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyDatabase.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Services
{
    public class QuestionViewService : IQuestionViewService
    {
        public CreateQuestionView CreateQuestion(IEnumerable<QuestionDifficulty> difficulties, IEnumerable<Topic> topics,IEnumerable<Certificate> certificates)
        {
            var newQV = new CreateQuestionView();
            return newQV;
        }
    }
}

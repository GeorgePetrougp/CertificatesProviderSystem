using WebApp.DTO_Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyDatabase.Models;

namespace WebApp.Services
{
    public class QuestionViewService : IQuestionViewService
    {
        public QuestionView CreateQuestion(IEnumerable<QuestionDifficulty> difficulties, IEnumerable<Topic> topics,IEnumerable<Certificate> certificates)
        {
            var newQV = new QuestionView
            {
                DifficultiesList = new QuestionDifficultyView
                {
                    Difficulties = new SelectList(difficulties, "QuestionDifficultyId", "Difficulty")
                },
                Topics = new SelectList(topics, "TopicId", "Title"),
                Certificates = new SelectList(certificates,"CertificateId","Title")
                
            };

            return newQV;
        }
    }
}

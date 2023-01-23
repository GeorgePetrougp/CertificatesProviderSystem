using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.DTO_Models
{
    public class MainQuestionVM
    {
        [ValidateNever]
        public QuestionView QuestionsView { get; set; }
        //[ValidateNever]
        //public QuestionDifficultyView DifficultiesView { get; set; }
        //[ValidateNever]
        //public TopicView TopicView { get; set; }
        //[ValidateNever]
        //public AnswerView AnswerView { get; set; }
        //[ValidateNever]
        //public CertificatesView CertificatesView { get; set; }

    }
}

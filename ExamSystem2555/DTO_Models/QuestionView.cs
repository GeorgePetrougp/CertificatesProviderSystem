using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.DTO_Models
{
    public class QuestionView
    {
        public int QuestionId { get; set; }
        public string Display { get; set; }
        //public int SelectedDifficultyId { get; set; }
        //public SelectList DifficultiesList { get; set; }
        [ValidateNever]
        public QuestionDifficultyView DifficultiesList { get; set; }
        [ValidateNever]
        public int SelectedTopicsId { get; set; }

        [ValidateNever]
        public SelectList? Topics { get; set; }
        public QuestionAnswerView AnswerA { get; set; }
        public QuestionAnswerView AnswerB { get; set; }
        public QuestionAnswerView AnswerC { get; set; }
        public QuestionAnswerView AnswerD { get; set; }

        public int SelectedCertificateId { get; set; }

        [ValidateNever]
        public SelectList? Certificates { get; set; }



    }
}

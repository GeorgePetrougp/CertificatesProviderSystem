using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyDatabase.Models;

namespace WebApp.DTO_Models
{
    public class EditQuestionView
    {
        [ValidateNever]

        public int EditQuestionId { get; set; }
        [ValidateNever]

        public string EditQuestionDisplay { get; set; }
        [ValidateNever]

        public string EditDifficultyLevel { get; set; }
        [ValidateNever]

        public QuestionDifficultyView EditDifficulty { get; set; }
        [ValidateNever]

        public List<AnswerView> AnswerViews { get; set; }
        [ValidateNever]

        public List<Topic> Topics { get; set; }
        [ValidateNever]

        public List<int> TopicIds { get; set; }
        [ValidateNever]

        public MultiSelectList TopicsList { get; set; }
        [ValidateNever]

        public List<Certificate> Certificates { get; set; }
        [ValidateNever]

        public List<int> CertificateIds { get; set; }
        [ValidateNever]

        public MultiSelectList CertificateList { get; set; }



        public EditQuestionView()
        {
            EditDifficulty= new QuestionDifficultyView();
            AnswerViews = new List<AnswerView>();
            Topics = new List<Topic>();
            Certificates = new List<Certificate>();
        }
    }
}

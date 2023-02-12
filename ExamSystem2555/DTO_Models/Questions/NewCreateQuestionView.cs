using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.DTO_Models.Questions
{
    public class NewCreateQuestionView
    {
        public CreateQuestionDTO Question { get; set; }
        [ValidateNever]
        public SelectList DifficultiesList { get; set; }
        public int SelectedDifficultyId { get; set; }
        [ValidateNever]
        public MultiSelectList TopicsList { get; set; }
        [ValidateNever]
        public List<int> SelectedTopicIds { get; set; }
        [ValidateNever]
        public MultiSelectList CertificatesList { get; set; }
        [ValidateNever]
        public List<int> SelectedCertificateIds { get; set; }
        public bool HasTopic { get; set; }

        public NewCreateQuestionView()
        {
            Question = new CreateQuestionDTO();
        }



    }
}

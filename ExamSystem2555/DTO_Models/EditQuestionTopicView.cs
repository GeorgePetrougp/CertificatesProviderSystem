using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyDatabase.Models;

namespace WebApp.DTO_Models
{
    public class EditQuestionTopicView
    {
        [ValidateNever]

        public int? QuestionId { get; set; }
        [ValidateNever]

        public List<Topic> Topics { get; set; }
        [ValidateNever]

        public MultiSelectList TopicsList { get; set; }
        [ValidateNever]
        public List<int> SelectedTopicIds { get; set; }
    }
}

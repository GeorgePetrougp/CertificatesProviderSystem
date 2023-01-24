using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.DTO_Models
{
    public class TopicView
    {
        public List<int>? SelectedTopicIds { get; set; }
        public MultiSelectList TopicsList { get; set; }
    }
}

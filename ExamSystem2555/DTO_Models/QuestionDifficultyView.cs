using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.DTO_Models
{
    public class QuestionDifficultyView
    {
        public int SelectedId { get; set; }
        public SelectList Difficulties { get; set; }
    }
}

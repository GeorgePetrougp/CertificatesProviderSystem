    using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO_Models
{
    public class QuestionDifficultyView
    {
        [Required(ErrorMessage = "Difficulty Is Required!")]
        public int SelectedId { get; set; }
        public SelectList Difficulties { get; set; }
    }
}

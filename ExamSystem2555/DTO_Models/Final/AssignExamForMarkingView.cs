using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.DTO_Models.Final
{
    public class AssignExamForMarkingView
    {
        public int CandidateExaminationId { get; set; }
        public SelectList? Markers { get; set; }
        public string SelectedMarkerId { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using MyDatabase.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO_Models
{
    public class LoginView
    {
        public int CandidateId { get; set; }
        public string LastName { get; set; }
        public SelectList CertificatesList { get; set; }
        public Certificate? Certificate { get; set; }
        public int? CertificateId { get; set; }
        public int SelectedId { get; set; }
        public DateTime ExamDate { get; set; }
    }
}

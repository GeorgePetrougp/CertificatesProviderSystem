using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyDatabase.Models;

namespace WebApp.DTO_Models
{
    public class EditCertAndTopicsView
    {
        [ValidateNever]

        public List<Topic>? CurrentTopicsList { get; set; }
        [ValidateNever]

        public List<Certificate>? CurrentCertificateList { get; set; }
        [ValidateNever]

        public MultiSelectList TopicsList { get; set; }
        [ValidateNever]

        public MultiSelectList CertificateList { get; set; }
        [ValidateNever]
        public List<int> SelectedTopics { get; set; }
        [ValidateNever]
        public List<int> SelectedCertificates { get; set; }


        public EditCertAndTopicsView()
        {
            SelectedCertificates = new List<int>();
            SelectedTopics = new List<int>();
        }
    }
}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [DisplayName("First Name")]     
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }

    }
}

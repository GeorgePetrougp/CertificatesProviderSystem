using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.DTO_Models.Final
{
    public class UserRolesView
    {
       public string UserId { get; set; }
        public List<string>? Roles { get; set; }
        public SelectList? RolesSelectList { get; set; }
        public int SelectedRoleId { get; set; }

    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyDatabase.Data;
using System.Data;
using WebApp.Data;
using WebApp.DTO_Models.Final;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class UserRolesController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        
        

        public UserRolesController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public IActionResult Index()
        {
            var users = _userManager.Users;
            return View(users);
        }
        public async Task<IActionResult> GetUserRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userRoles = await _userManager.GetRolesAsync(user);
            var model = new UserRolesView
            {
                UserId = id,
                Roles = userRoles.ToList(),

            };
            return View(model);
        }

        public IActionResult Roles(string id) 
        {

            var roles =  _roleManager.Roles;
            var model = new UserRolesView
            {
                UserId= id,
                RolesSelectList = new SelectList(roles.OrderBy(x=>x.Name),"Id","Name")

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Roles(string userId,string selectedRoleId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByIdAsync(selectedRoleId);

            if (user == null || role == null)
            {
                return BadRequest();
            }

            var userRoles =await _userManager.GetRolesAsync(user);
            
            if (!userRoles.Contains(role.Name))
            {
                var result = await _userManager.AddToRoleAsync(user, role.NormalizedName);

                if (!result.Succeeded)
                {
                    return BadRequest();
                }
            }
            else
            {
                var result = await _userManager.RemoveFromRoleAsync(user, role.NormalizedName);
                result = await _userManager.AddToRoleAsync(user, role.NormalizedName);

                if (!result.Succeeded)
                {
                    return BadRequest();
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteRoles(string userId,string item)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByNameAsync(item);
            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");


        }
    }
}

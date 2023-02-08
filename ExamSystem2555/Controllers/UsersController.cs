using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class UsersController : Controller
    {

        private readonly ICandidateService _candidateService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;


        public UsersController(ICandidateService candidateService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _candidateService = candidateService;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        // GET: UsersController
        public async Task<ActionResult> CandidateRegistration(int? id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var x = (ClaimsIdentity)User.Identity;
                var y = x.FindFirst(ClaimTypes.NameIdentifier);
                var z = y.Value;
                var user = await _userManager.FindByIdAsync(z);
                var userRoles = await _userManager.GetRolesAsync(user);
                if (!userRoles.Contains("Candidate"))
                {

                    return View();
                }
                return RedirectToAction("SelectCertificate","Candidates");
            }
            return Redirect("/Identity/Account/Login");

        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

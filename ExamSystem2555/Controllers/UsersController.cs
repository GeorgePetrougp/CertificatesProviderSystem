using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyDatabase.Models;
using System.Data;
using System.Security.Claims;
using System.Text;
using WebApp.DTO_Models;
using WebApp.MainServices;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class UsersController : Controller
    {

        private readonly IEShopService _service;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;


        public UsersController(IEShopService service, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _service= service;
            _signInManager = signInManager;
            _userManager = userManager;
        }

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
                return RedirectToAction("SelectCertificate");
            }
            return Redirect("/Identity/Account/Login");

        }

        public async Task<IActionResult> SelectCertificate(int candidateId)
        {
            var certificatesList = await _service.CertificateService.GetAllCertificatesAsync();
            var model = new LoginView
            {
                CandidateId = candidateId,
                CertificatesList = new SelectList(certificatesList, "CertificateId", "Title")
            };
            //model.CertificatesList = new SelectList(certificates, "CertificateId", "Title");
            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> SelectCertificate([FromForm] LoginView model, int? candidateId)
        {
            var exams = (await _service.ExaminationService.GetAllExaminationsAsync()).ToList();
            var certificate = await _service.CertificateService.GetCertificateByIdAsync(model.SelectedId);
            var myExamInts = exams.Where(c => c.Certificate == certificate).Select(e => e.ExaminationId).ToList();

            Random random = new Random();
            int randomIndex = random.Next(myExamInts[0], myExamInts.Count() - 1);
            var myExam = await _service.ExaminationService.GetExaminationByIdAsync(randomIndex);

            var candidate = await _service.CandidateService.GetCandidateByIdAsync(1);

            //duplicate check
            Random random2 = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 3; i++)
            {
                char letter = (char)random2.Next(65, 91);
                sb.Append(letter);
            }

            for (int i = 0; i < 5; i++)
            {
                int number = random2.Next(0, 10);
                sb.Append(number);
            }

            string generatedString = sb.ToString();

            //TO DO RANDOM GENERATOR FOR EXAMCODE
            var newCandidateExam = new CandidateExam()
            {
                Candidate = candidate,
                ExamCode = generatedString,
                ExamDate = model.ExamDate,
                Examination = myExam
            };


            await _service.CandidateExamService.AddCandidateExamAsync(newCandidateExam);
            await _service.SaveChangesAsync();

            return RedirectToAction("ExaminationSystemIndex", "ExaminationSystem", new { candidateExamId = newCandidateExam.CandidateExamId });
        }


        // GET: UsersController
            
    }
}

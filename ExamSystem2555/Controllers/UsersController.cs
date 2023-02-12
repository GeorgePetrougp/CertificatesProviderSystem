using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyDatabase.Models;
using System.Data;
using System.Security.Claims;
using System.Text;
using WebApp.DTO_Models;
using WebApp.MainServices.Interfaces;
using WebApp.Models;
using WebApp.Services;
using WebApp.Services.Interfaces;

namespace WebApp.Controllers
{
    public class UsersController : Controller
    {

        private readonly IEShopService _service;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICandidateService _candidateService;



        public UsersController(IEShopService service, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ICandidateService candidateService)
        {
            _service = service;
            _signInManager = signInManager;
            _userManager = userManager;
            _candidateService = candidateService;
        }

        public async Task<ActionResult> CandidateRegistration(int? id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                var user = await _userManager.FindByIdAsync(userId);
                var userRoles = await _userManager.GetRolesAsync(user);
                if (!userRoles.Contains("Candidate"))
                {
                    return View();
                }
                return RedirectToAction("SelectCertificate",new {id=id});
            }
            return Redirect("/Identity/Account/Login");

        }

        public async Task<IActionResult> SelectCertificate(int? id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var candidate = await _service.CandidateService.GetCandidateByUserAsync(userId);
            var certificatesList = await _service.CertificateService.GetAllCertificatesAsync();
            var certificate = await _service.CertificateService.GetCertificateByIdAsync(id);
            var model = new LoginView
            {
                CandidateId = candidate.CandidateId,
                CertificatesList = new SelectList(certificatesList, "CertificateId", "Title"),
                Certificate=certificate,
                CertificateId=id
            };
            //model.CertificatesList = new SelectList(certificates, "CertificateId", "Title");
            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> SelectCertificate([FromForm] LoginView model,int CertificateId)
        {
            var exams = (await _service.ExaminationService.GetAllExaminationsAsync()).ToList();
            var certificate = await _service.CertificateService.GetCertificateByIdAsync(CertificateId);
            var myExamInts = exams.Where(c => c.Certificate == certificate).Select(e => e.ExaminationId).ToList();

            Random random = new Random();
            int randomIndex = random.Next(myExamInts[0], myExamInts.Count - 1);
            var myExam = await _service.ExaminationService.GetExaminationByIdAsync(randomIndex);

            var candidate = await _service.CandidateService.GetCandidateByIdAsync(model.CandidateId);

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
            var newCandidateExam = new CandidateExamination()
            {
                Candidate = candidate,
                ExamCode = generatedString,
                ExamDate = model.ExamDate,
                Examination = myExam,
            };


            await _service.CandidateExamService.AddCandidateExamAsync(newCandidateExam);
            await _service.SaveChangesAsync();

            return RedirectToAction("CandidateExaminationVouchers", "CandidateExaminations", new { candidateId = model.CandidateId });
        }

    }
}

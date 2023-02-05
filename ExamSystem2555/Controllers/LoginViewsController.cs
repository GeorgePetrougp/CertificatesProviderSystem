using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.DTO_Models;
using WebApp.Data;
using WebApp.Services;
using MyDatabase.Models;

namespace WebApp.Controllers
{
    public class LoginViewsController : Controller
    {
        private readonly ICertificateService _certService;
        private readonly ICandidateService _candidateSertvice;
        private readonly IExaminationService _examService;

        private readonly ApplicationDbContext _context;

        public LoginViewsController(ICertificateService certService, ICandidateService candidateService, IExaminationService examService, ApplicationDbContext context)
        {
            _certService = certService;
            _candidateSertvice = candidateService;
            _examService = examService;
            _context = context;
        }


        // GET: LoginViews/Create
        public IActionResult Login()
        {
            _context.
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginView model)
        {
            if (model.CandidateId == 0)
            {
                ModelState.AddModelError("candidateId", "Please enter your candidate ID.");
                return View();
            }
            var candidate = _context.Candidates.FirstOrDefault(c => c.CandidateId == model.CandidateId);
            if (candidate == null)
            {
                ModelState.AddModelError("candidateId", "Invalid candidate ID.");
                return View();
            }
            // login logic
            return RedirectToAction("SelectCertificate", "LoginViews", new { candidateId = model.CandidateId });
        }

        public async Task<IActionResult> SelectCertificate(int candidateId)
        {
            var certificatesList = await _certService.GetAllCertificatesAsync();
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
            var myExam = await _examService.GetExaminationByIdAsync(1);
            var candidate = await _candidateSertvice.GetCandidateByIdAsync(candidateId);
            var certificate = await _certService.GetCertificateByIdAsync(model.SelectedId);
            var newCandidateExam = new CandidateExam()
            {
                Candidate = candidate,
                ExamCode = "GMTXS01",
                ExamDate = DateTime.Now.AddDays(30),
                Examination = myExam
                
            };


            _context.CandidateExams.Add(newCandidateExam);
            _context.SaveChanges();

            return RedirectToAction("Index","ExaminationView", new {candidateExamId = newCandidateExam.CandidateExamId} );
        }

    }
}

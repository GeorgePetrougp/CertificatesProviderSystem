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

namespace WebApp.Controllers
{
    public class LoginViewsController : Controller
    {
        private readonly ICertificateService _service;
        private readonly ApplicationDbContext _context;

        public LoginViewsController(ICertificateService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }


        // GET: LoginViews/Create
        public IActionResult Login()
        {
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
            return RedirectToAction("CertificateSelect", "LoginViews",model);
        }

        // GET: LoginViews/CertificateSelect
        public async Task<IActionResult> CertificateSelectAsync(LoginView model)
        {
            var certificates = await _service.GetAllCertificatesAsync();
            model.CertificatesList = new SelectList(certificates, "CertificateId", "Title");
            return View(model);
        }


        [HttpPost]
        public IActionResult CertificateSelect(LoginView model)
        {
                        
            // login logic
            return RedirectToAction("Index", "ExaminationView");
        }


 
      

       
    }
}

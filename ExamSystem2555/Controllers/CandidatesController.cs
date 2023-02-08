using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using System.Security.Claims;
using System.Text;
using WebApp.DTO_Models;
using WebApp.DTO_Models.Candidates;
using WebApp.MainServices;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly ICandidateManagerService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public CandidatesController(ICandidateManagerService service, IMapper mapper,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _service = service;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        // GET: CandidatesController
        public async Task<IActionResult> CandidatesIndex()
        {
            var candidates = await _service.CandidateService.GetAllCandidatesAsync();
            return View(candidates);
        }

        // GET: CandidatesController/Details/5
        public async Task<IActionResult> CandidateDetails(int id)
        {
            var candidate = await _service.CandidateService.GetCandidateByIdAsync(id);
            await _service.LoadCandidateAddress(candidate);
            return View(candidate);
        }

        // GET: CandidatesController/Create
        public async Task<IActionResult> CreateCandidate()
        {
            var model = new CandidateDTO();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveCandidate(CandidateDTO candidate)
        {
            var x = (ClaimsIdentity)User.Identity;
            var y = x.FindFirst(ClaimTypes.NameIdentifier);
            var z = y.Value;
            var newCandidate = _mapper.Map<Candidate>(candidate);
            newCandidate.UserCandidateId = z;
            var user = await _userManager.FindByIdAsync(z);
            var role = await _roleManager.FindByNameAsync("Candidate");
            if (ModelState.IsValid)
            {
                await _service.CandidateService.AddCandidateAsync(newCandidate);
                var result = await _userManager.AddToRoleAsync(user, role.NormalizedName);
                await _service.SaveChangesAsync();
                return RedirectToAction("CandidatesIndex");
            }

            return View(ModelState);
        }


        // GET: CandidatesController/Edit/5
        public async Task<IActionResult> EditCandidate(int id)
        {
            var candidate = await _service.CandidateService.GetCandidateByIdAsync(id);
            var model = _mapper.Map<CandidateDTO>(candidate);

            return View(model);
        }

        // POST: CandidatesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCandidate(CandidateDTO candidate)
        {
            var updatedCandidate = _mapper.Map<Candidate>(candidate);

            if (ModelState.IsValid)
            {
                await _service.CandidateService.UpdateCandidateAsync(updatedCandidate);
                await _service.SaveChangesAsync();
                return RedirectToAction("CandidatesIndex");
            }

            return View(ModelState);
        }

        public async Task<IActionResult> CandidateAddressesIndex(int id)
        {
            var candidate = await _service.CandidateService.GetCandidateByIdAsync(id);
            
            var addressList = (await _service.AddressService.GetAllAddressesAsync()).Where(a=>a.Candidate==candidate).ToList();  

            return View(addressList);
        }

        public async Task<IActionResult> EditCandidateAddresses(int id)
        {
            var address = await _service.AddressService.GetAddressByIdAsync(id);
            var addressDTO = _mapper.Map<AddressDTO>(address);

            return View(addressDTO);
        }
        [HttpPost]
        public async Task<IActionResult> EditCandidateAddresses(AddressDTO address)
        {
            var newAddress = _mapper.Map<Address>(address);
            if(ModelState.IsValid)
            {
                await _service.AddressService.UpdateAddressAsync(newAddress);
                await _service.SaveChangesAsync();
                return RedirectToAction("CandidatesIndex");
            }

            return View(ModelState);
        }



        // GET: CandidatesController/Delete/5
        public ActionResult DeleteCandidate(int id)
        {

            return View();
        }

        // POST: CandidatesController/Delete/5
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
            var myExamInts = exams.Where(c => c.Certificate == certificate).Select(e=>e.ExaminationId).ToList();

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

            return RedirectToAction("Index", "ExaminationView", new { candidateExamId = newCandidateExam.CandidateExamId });
        }

        
    }
}

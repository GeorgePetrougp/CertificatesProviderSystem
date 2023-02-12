using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using System.Data;
using System.Security.Claims;
using System.Text;
using WebApp.DTO_Models;
using WebApp.DTO_Models.Candidates;
using WebApp.MainServices.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly ICandidateManagerService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public CandidatesController(ICandidateManagerService service, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _service = service;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        // GET: CandidatesController
        [Authorize(Roles = "Quality Controller,Administrator")]
        public async Task<IActionResult> CandidatesIndex()
        {
            var candidates = await _service.CandidateService.GetAllCandidatesAsync();
            return View(candidates);
        }

        // GET: CandidatesController/Details/5
        [Authorize(Roles = "Quality Controller,Administrator")]
        public async Task<IActionResult> CandidateDetails(int id)
        {
            var candidate = await _service.CandidateService.GetCandidateByIdAsync(id);
            await _service.LoadCandidateAddress(candidate);
            return View(candidate);
        }

        public async Task<IActionResult> AdministratorCreateCandidate(string userId)
        {

            var model = new CandidateDTO
            {
                UserCandidateId = userId,
                FirstName = (await _userManager.FindByIdAsync(userId)).FirstName,
                LastName = (await _userManager.FindByIdAsync(userId)).LastName,

            };
            return View("CreateCandidate", model);
        }


        // GET: CandidatesController/Create
        public async Task<IActionResult> CreateCandidate()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var model = new CandidateDTO 
            { 
                UserCandidateId = userId,
                FirstName = (await _userManager.FindByIdAsync(userId)).FirstName,
                LastName = (await _userManager.FindByIdAsync(userId)).LastName

            };

            return View("CreateCandidate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveCandidate(CandidateDTO candidate)
        {
            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claimValue = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Issuer;

            var newCandidate = _mapper.Map<Candidate>(candidate);
            //newCandidate.UserCandidateId = claimValue;
            var user = await _userManager.FindByIdAsync(candidate.UserCandidateId);
            var role = await _roleManager.FindByNameAsync("Candidate");
            if (ModelState.IsValid)
            {
                await _service.CandidateService.AddCandidateAsync(newCandidate);
                await _userManager.AddToRoleAsync(user, role.NormalizedName);
                await _service.SaveChangesAsync();

                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                var userr = await _userManager.FindByIdAsync(userId);
                var userRoles = await _userManager.GetRolesAsync(userr);
                if (userRoles.Contains("Administrator"))
                {
                    return RedirectToAction("CandidatesIndex");

                }
                else
                {
                    return RedirectToAction("Index", "GeneralHome");
                }
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

            var addressList = (await _service.AddressService.GetAllAddressesAsync()).Where(a => a.Candidate == candidate).ToList();

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
            var newAddress = _mapper.Map<CandidateAddress>(address);
            if (ModelState.IsValid)
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

    }
}

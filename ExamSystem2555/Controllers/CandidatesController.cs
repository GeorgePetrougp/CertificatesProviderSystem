using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDatabase.Models;
using WebApp.DTO_Models.Candidates;
using WebApp.MainServices;

namespace WebApp.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly ICandidateManagerService _service;
        private readonly IMapper _mapper;

        public CandidatesController(ICandidateManagerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
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
            var newCandidate = _mapper.Map<Candidate>(candidate);

            if (ModelState.IsValid)
            {
                await _service.CandidateService.AddCandidateAsync(newCandidate);
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
    }
}

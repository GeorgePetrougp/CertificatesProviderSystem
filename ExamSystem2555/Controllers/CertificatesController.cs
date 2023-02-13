using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApp.DTO_Models.Certificates;
using WebApp.MainServices.Interfaces;

namespace WebApp.Controllers
{
    public class CertificatesController : Controller
    {
        private readonly ICertificateManagerService _service;
        private readonly IMapper _mapper;

        public CertificatesController(ICertificateManagerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [Authorize(Roles = "Quality Controller,Administrator")]
        public async Task<IActionResult> CertificatesIndex()
        {
            var certificates = await _service.CreateCertificateDTOs();
            return View(certificates);
        }

        [Authorize(Roles = "Quality Controller,Administrator")]
        public async Task<IActionResult> CertificateDetails(int? id)
        {

            if (await _service.NullValidation(id))
            {
                return NotFound();
            }

            var certificate = await _service.CreateCertificateDTO(id);

            return View(certificate);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateCertificate()
        {
            var createCertificateView = await _service.CreateCertificateView();
            return View(createCertificateView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCertificate(CreateCertificateView certificate)
        {
            var newCertificate = await _service.TBD(certificate);

            if (ModelState.IsValid)
            {
                await _service.AddCertificate(newCertificate);

                return RedirectToAction("CertificatesIndex");
            }
            return RedirectToAction("CreateCertificate");
        }

        // GET: Certificates/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditCertificate(int? id)
        {
            if (await _service.NullValidation(id))
            {
                return NotFound();
            }

            var editCertificateView = await _service.CreateCertificateView(id);

            return View(editCertificateView);
        }

        // POST: Certificates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCertificate(CreateCertificateView certificate)
        {
            var editedCertificate = await _service.TBD(certificate);


            if (ModelState.IsValid)
            {
                await _service.UpdateCertificate(editedCertificate);

                return RedirectToAction("CertificatesIndex");
            }
            return RedirectToAction("EditCertificate", certificate.CertificateDTO.CertificateId);
        }


        // GET: Certificates/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DisableCertificate(int? id)
        {
            if (id == null || await _service.CertificateService.GetAllCertificatesAsync() == null)
            {
                return NotFound();
            }

            var certificate = await _service.CertificateService.GetCertificateByIdAsync(id);

            if (certificate == null)
            {
                return NotFound();
            }
            var certificateDTO = await _service.CreateCertificateDTO(id);

            return View(certificateDTO);
        }

        // POST: Certificates/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisableCertificate(int id)
        {
            if (await _service.CertificateService.GetAllCertificatesAsync() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Certificates'  is null.");
            }
            var certificate = await _service.CertificateService.GetCertificateByIdAsync(id);

            if (certificate != null)
            {
                certificate.Status = "Unavailable";
                await _service.CertificateService.UpdateCertificateAsync(certificate);
            }

            await _service.SaveChangesAsync();
            return RedirectToAction("CertificatesIndex");
        }

        private async Task<bool> CertificateExistsAsync()
        {
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using WebApp.Data;
using WebApp.DTO_Models.Certificates;
using WebApp.MainServices;

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

        public async Task<IActionResult> CertificatesIndex()
        {
            var certificates = await _service.CreateCertificateDTOs();
            return View(certificates);
        }

        public async Task<IActionResult> CertificateDetails(int? id)
        {
            if (await _service.NullValidation(id))
            {
                return NotFound();
            }

            var certificate = await _service.CreateCertificateDTO(id);

            return View(certificate);
        }

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
                //try
                //{
                    await _service.UpdateCertificate(editedCertificate);
                //}

                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!await CertificateExistsAsync(certificate.CertificateDTO.CertificateId))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                return RedirectToAction("CertificatesIndex");
            }
            return RedirectToAction("EditCertificate",certificate.CertificateDTO.CertificateId);
        }

        // GET: Certificates/Delete/5
        public async Task<IActionResult> DeleteCertificate(int? id)
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

            return View(certificate);
        }

        // POST: Certificates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _service.CertificateService.GetAllCertificatesAsync() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Certificates'  is null.");
            }
            var certificate = await _service.CertificateService.GetCertificateByIdAsync(id);

            if (certificate != null)
            {
                await _service.CertificateService.DeleteCertificateAsync(id);
            }

            await _service.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CertificateExistsAsync()
        {
            return true;
        }
    }
}

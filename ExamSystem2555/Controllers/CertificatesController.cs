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

        // GET: Certificates
        public async Task<IActionResult> CertificatesIndex()
        {
            var certificates = await _service.CertificateService.GetAllCertificatesAsync();
            return View(certificates);
        }

        // GET: Certificates/Details/5
        public async Task<IActionResult> CertificateDetails(int? id)
        {
            if (id == null || _service.CertificateService.GetAllCertificatesAsync() == null)
            {
                return NotFound();
            }

            var certificate = await _service.CertificateService.GetCertificateByIdAsync(id);
            await _service.LoadLevel(certificate);

            if (certificate == null)
            {
                return NotFound();
            }


            var model = new CertificateDetailsView
            {
                Certificate = _mapper.Map<CertificateDTO>(certificate),
                LevelTitle = certificate.Level.Title
            };

            return View(model);
        }

        // GET: Certificates/Create
        public async Task<IActionResult> CreateCertificate()
        {
            var levelsList = await _service.LevelService.GetAllLevelsAsync();

            var model = new CreateCertificateView
            {
                CertificateLevelsList = new SelectList(levelsList, "LevelId", "Title")
            };
            return View(model);
        }

        // POST: Certificates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCertificate(CreateCertificateView certificate)
        {
            var level = await _service.LevelService.GetLevelByIdAsync(certificate.SelectedLevelId);

            var newCertificate = _mapper.Map<Certificate>(certificate.CertificateDTO);
            newCertificate.Level = level;

            if (ModelState.IsValid)
            {
                await _service.CertificateService.AddCertificateAsync(newCertificate);
                await _service.SaveChangesAsync();
                return RedirectToAction("CertificatesIndex");
            }
            return View(certificate);
        }

        // GET: Certificates/Edit/5
        public async Task<IActionResult> EditCertificate(int? id)
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

            var levelsList = await _service.LevelService.GetAllLevelsAsync();


            var editCertificateView = new CreateCertificateView
            {
                CertificateDTO = _mapper.Map<CertificateDTO>(certificate),
                CertificateLevelsList = new SelectList(levelsList, "LevelId", "Title")
            };


            return View(editCertificateView);
        }

        // POST: Certificates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CertificateEdit(CreateCertificateView certificate)
        {
            var editedCertificate = _mapper.Map<Certificate>(certificate.CertificateDTO);
            editedCertificate.Level = await _service.LevelService.GetLevelByIdAsync(certificate.SelectedLevelId);

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.CertificateService.UpdateCertificateAsync(editedCertificate);
                    await _service.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CertificateExistsAsync(certificate.CertificateDTO.CertificateId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("CertificatesIndex");
            }
            return View(certificate);
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

        private async Task<bool> CertificateExistsAsync(int id)
        {
            return true;
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MyDatabase.Models;
using System.Data;
using System.Security.Claims;
using WebApp.DTO_Models.CandidateExaminations;
using WebApp.DTO_Models.Final;
using WebApp.MainServices;
using WebApp.MainServices.Interfaces;
using WebApp.Services.Interfaces;

namespace WebApp.Controllers
{
    public class CandidateExaminationsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICandidateExaminationManagerService _service;
        private readonly ICandidateService _candidateService;
        //private readonly ICandidateManagerService _candidateService;

        public CandidateExaminationsController(IMapper mapper, ICandidateExaminationManagerService service, ICandidateService candidateService)
        {
            _mapper = mapper;
            _service = service;
            _candidateService = candidateService;
        }

        public async Task<ActionResult> CandidateExaminationsIndex()
        {
            var candidateExaminations = await _service.CandidateExamService.GetAllCandidateExamAsync();
            await _service.CandidateExaminationLoad(candidateExaminations);
            var model = _mapper.Map<List<CandidateExaminationsDTO>>(candidateExaminations);
            return View(model);
        }

        public async Task<ActionResult> CandidateExaminationVouchers(int candidateId)
        {
            var candidate = await _candidateService.GetCandidateByIdAsync(candidateId);
            var candidateExaminations = (await _service.CandidateExamService.GetAllCandidateExamAsync()).Where(ca=>ca.Candidate==candidate);

            var vouchers = candidateExaminations
                .Where(ca => ca.CandidateExamResults == null)
                .Select(x => _mapper.Map<CandidateExaminationsDTO>(x));

            return View("CandidateVouchers",vouchers);

        }


        public async Task<ActionResult> CandidateExaminationsIndexByCandidate()
        {
            //var _candidateService = serviceProvider.GetRequiredService<ICandidateService>();
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var candidate = await _candidateService.GetCandidateByUserAsync(userId);

            var candidateExaminations = (await _service.CandidateExamService.GetAllCandidateExamAsync()).Where(ce => ce.Candidate == candidate);

            var candidateExaminationsList = new List<CandidateExaminationsDTO>();
            foreach (var examination in candidateExaminations)
            {
                await _service.CandidateResultsLoad(examination);
                var candidateExamination = _mapper.Map<CandidateExaminationsDTO>(examination);
                candidateExaminationsList.Add(candidateExamination);

            }

            return View("candidateExaminationsIndex",candidateExaminationsList);
        }


        public async Task<ActionResult> CandidateExaminationDetails(int? id)
        {
            var candidateExamination = await _service.CandidateExamService.GetCandidateExamByIdAsync(id);
            await _service.CandidateExaminationLoad(candidateExamination);

            var model = _mapper.Map<CandidateExaminationsDTO>(candidateExamination);

            return View(model);

        }
        

    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MyDatabase.Models;
using System.Data;
using WebApp.DTO_Models.CandidateExaminations;
using WebApp.DTO_Models.Final;
using WebApp.MainServices;
using WebApp.MainServices.Interfaces;

namespace WebApp.Controllers
{
    public class CandidateExaminationsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICandidateExaminationManagerService _service;

        public CandidateExaminationsController(IMapper mapper, ICandidateExaminationManagerService service)
        {
            _mapper = mapper;
            _service = service;
        }

        public async Task<ActionResult> CandidateExaminationsIndex()
        {
            var candidateExaminations = await _service.CandidateExamService.GetAllCandidateExamAsync();
            await _service.CandidateExaminationLoad(candidateExaminations);
            var model = _mapper.Map<List<CandidateExaminationsDTO>>(candidateExaminations);
            return View(model);
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

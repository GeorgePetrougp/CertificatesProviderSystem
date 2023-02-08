using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MyDatabase.Models;
using System.Data;
using WebApp.DTO_Models.CandidateExaminations;
using WebApp.DTO_Models.Final;
using WebApp.MainServices;

namespace WebApp.Controllers
{
    public class CandidateExaminationsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IExamManagerService _service;

        public CandidateExaminationsController(IMapper mapper, IExamManagerService service)
        {
            _mapper = mapper;
            _service = service;
        }

        public async Task<ActionResult> CandidateExaminationsIndex()
        {
            var candidateExaminations = await _service.CandidateExamService.GetAllCandidateExamAsync();
            await _service.CandidateExamLoad(candidateExaminations);
            var model = _mapper.Map<List<CandidateExaminationsDTO>>(candidateExaminations);
            return View(model);
        }

        public async Task<ActionResult> CandidateExaminationDetails(int? id)
        {
            var candidateExamination = await _service.CandidateExamService.GetCandidateExamByIdAsync(id);
            await _service.CandidateExamLoad(candidateExamination);

            var model = _mapper.Map<CandidateExaminationsDTO>(candidateExamination);

            return View(model);

        }
        

    }
}

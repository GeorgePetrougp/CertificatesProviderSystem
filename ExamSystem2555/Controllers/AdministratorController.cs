using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.DTO_Models.CandidateExaminations;
using WebApp.DTO_Models.Final;
using WebApp.MainServices.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAdministratorService _service;
        private readonly IMapper _mapper;

        public AdministratorController(IAdministratorService service, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _service = service;
            _userManager = userManager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> CandidateExaminationsPendingForMarkingIndex()
        {
            var candidateResults = (await _service.CandidateExamResultsService.GetAllCandidateExamResultsAsync()).ToList();
            await _service.CandidateExamResultsLoad(candidateResults);
            var candidateExaminations = candidateResults.Select(c => c.CandidateExam);
            await _service.CandidateExamLoad(candidateExaminations);
            var model = _mapper.Map<List<CandidateExaminationsDTO>>(candidateExaminations);
            return View(model);
        }

        public async Task<IActionResult> AssignExaminationForMarking(int id)
        {
            var markers = await _userManager.GetUsersInRoleAsync("Marker");
            var model = new AssignExamForMarkingView
            {
                CandidateExaminationId = id,
                Markers = new SelectList(markers, "Id", "LastName"),

            };

            return View(model);


        }

        [HttpPost]
        public async Task<IActionResult> AssignExaminationForMarking(AssignExamForMarkingView model)
        {
            var candidateExamination = await _service.CandidateExamService.GetCandidateExamByIdAsync(model.CandidateExaminationId);
            var user = await _userManager.FindByIdAsync(model.SelectedMarkerId);
            var result = new MarkerAssignedExam
            {
                ApplicationUser = user,
                CandidateExam = candidateExamination
            };

            await _service.MarkerAssignedExamService.AddMarkerAssignedExamAsync(result);
            await _service.SaveChangesAsync();

            return RedirectToAction("CandidateExaminationsPendingForMarkingIndex");
        }
    }
}

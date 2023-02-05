using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDatabase.Models;
using WebApp.DTO_Models.Final;
using WebApp.MainServices;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class ExaminationsController : Controller
    {
        private readonly IExamManagerService _service;

        public ExaminationsController(IExamManagerService examService)
        {
            _service = examService;
        }
        // GET: ExaminationsController
        public async Task<ActionResult> Index()
        {
            var x = (await _service.CandidateExamService.GetAllCandidateExamAsync()).ToList();

            var list = new List<ExaminationControllerView>();

            foreach (var item in x)
            {
                await _service.CandidateResultsLoad(item);
                
                var y = new ExaminationControllerView
                {
                    CandidateExamId = item.CandidateExamId,
                    Candidate = item.Candidate,
                    ExamCode = item.ExamCode,
                    ExamDate = item.ExamDate,
                    ResultIssueDate = item.CandidateExamResults.ResultIssueDate,
                    ResultLabel = item.CandidateExamResults.ResultLabel

                };
                list.Add(y);
            }

            return View(list);
        }
     
    }
}

using WebApp.DTO_Models;
using WebApp.MainServices;
using WebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDatabase.Models;

namespace WebApp.Controllers
{
    public class ExaminationViewController : Controller
    {
        private readonly IExamManagerService _service;

        public ExaminationViewController(IExamManagerService service)
        {
            _service = service;
        }


        // GET: ExaminationViewController
        public ActionResult Index()
        {
            return View();
        }

       

        // GET: ExaminationViewController/Create
        public async Task<ActionResult> CreateAsync()
        {
            var examPage = new ExaminationView
            {
                
                Question = new ExamQuestionView
                {
                    Display = _service.QuestionService.GetQuestionByIdAsync(1).Result.Display,
                    QuestionId = _service.QuestionService.GetQuestionByIdAsync(1).Result.QuestionId
                },
                AnswerA = new ExamAnswerView
                {
                    AnswerId = _service.AnswerService.GetAnswerByIdAsync(1).Result.QuestionPossibleAnswerId,
                    Display = _service.AnswerService.GetAnswerByIdAsync(1).Result.PossibleAnswer
                },

                AnswerB = new ExamAnswerView
                {
                    AnswerId = _service.AnswerService.GetAnswerByIdAsync(2).Result.QuestionPossibleAnswerId,
                    Display = _service.AnswerService.GetAnswerByIdAsync(2).Result.PossibleAnswer
                },

                AnswerC = new ExamAnswerView
                {
                    AnswerId = _service.AnswerService.GetAnswerByIdAsync(3).Result.QuestionPossibleAnswerId,
                    Display = _service.AnswerService.GetAnswerByIdAsync(3).Result.PossibleAnswer
                },

                AnswerD = new ExamAnswerView
                {
                    AnswerId = _service.AnswerService.GetAnswerByIdAsync(4).Result.QuestionPossibleAnswerId,
                    Display = _service.AnswerService.GetAnswerByIdAsync(4).Result.PossibleAnswer
                }

            };

            return View(examPage);
        }

        // POST: ExaminationViewController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(ExaminationView examinationView)
        {
            var selectedAnswerList = new List<ExamAnswerView>();
            selectedAnswerList.Add(examinationView.AnswerA);
            selectedAnswerList.Add(examinationView.AnswerB);
            selectedAnswerList.Add(examinationView.AnswerC);
            selectedAnswerList.Add(examinationView.AnswerD);
            string finalAnswer = "";
            foreach(var item in selectedAnswerList)
            {
                if(item.SelectedAnswer == true)
                {
                    finalAnswer = item.Display;
                }
            }
            var answers = new ExamCandidateAnswer
            {

                SelectedAnswer = finalAnswer,
                CorrectAnswer = examinationView.AnswerB.Display,

            };
            await _service.CandidateAnswerService.AddExamCandidateAnswerAsync(answers);
            await _service.SaveChangesAsync();

            return View("Index");
        }

      


    }
}

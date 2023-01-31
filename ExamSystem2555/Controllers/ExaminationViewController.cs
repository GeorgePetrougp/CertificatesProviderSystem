using WebApp.DTO_Models;
using WebApp.MainServices;
using WebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDatabase.Models;
using Microsoft.EntityFrameworkCore;
using WebApp.DTO_Models.Final;

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
        public async Task<ActionResult> StartExamination()
        {
            var myExam = await _service.ExaminationService.GetExaminationByIdAsync(1);


            var selectedExaminationQuestions = (await _service.ExamQuestionService.GetAllExaminationQuestionsAsync()).Where(e => e.Examination == myExam).ToList();
            await _service.ExaminationQuestionLoad(selectedExaminationQuestions);
            selectedExaminationQuestions.Select(x => x.CertificateTopicQuestion.TopicQuestion.Question).ToList();
            List<Question> selectedQuestions = new List<Question>();
            selectedExaminationQuestions.ForEach(x => selectedQuestions.Add(x.CertificateTopicQuestion.TopicQuestion.Question));

            List<QuestionDTO> selectedQuestionDTOs = new List<QuestionDTO>();
            foreach (var item in selectedQuestions) 
            {
                selectedQuestionDTOs.Add(new QuestionDTO { QuestionId = item.QuestionId, QuestionDisplay = item.Display });
            }
            //var xy = (await _service.AnswerService.GetAllAnswersAsync()).Where(b => b.Question == item).ToList();
            //var posAnswers = _service.AnswerService.GetAllAnswersAsync();



            var myExamView = new ExaminationQuestionView()
            {
                Questions = selectedQuestionDTOs,
                CurrentQuestion = selectedQuestionDTOs.FirstOrDefault()

            };

            myExamView.Questions = selectedQuestionDTOs;
           
            return View(myExamView);

        }

        // POST: ExaminationViewController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> StartExamination(ExaminationView examinationView)
        {
            var selectedAnswerList = new List<ExamAnswerView>();
            selectedAnswerList.Add(examinationView.AnswerA);
            selectedAnswerList.Add(examinationView.AnswerB);
            selectedAnswerList.Add(examinationView.AnswerC);
            selectedAnswerList.Add(examinationView.AnswerD);
            string finalAnswer = "";
            foreach (var item in selectedAnswerList)
            {
                if (item.SelectedAnswer == true)
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

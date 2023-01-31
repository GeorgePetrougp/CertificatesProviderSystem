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
            var selectedQuestions = new List<Question>();
            selectedExaminationQuestions.ForEach(x => selectedQuestions.Add(x.CertificateTopicQuestion.TopicQuestion.Question));
            var possibleAnswers = new List<QuestionPossibleAnswer>();
            var selectedQuestionDTOs = new List<QuestionDTO>();
            
            foreach (var item in selectedQuestions)
            {
                var myAnswers = (await _service.AnswerService.GetAllAnswersAsync()).Where(a => a.Question == item);
                possibleAnswers.AddRange(myAnswers);

                List<QuestionPossibleAnswersDTO> possibleAnswersDTOs = new List<QuestionPossibleAnswersDTO>();
                possibleAnswers.ForEach(x=> possibleAnswersDTOs.Add(new QuestionPossibleAnswersDTO { QuestionPossibleAnswerId= x.QuestionPossibleAnswerId, QuestionPossibleAnswer=x.PossibleAnswer, IsAnswerCorrect=x.IsCorrect }));

                selectedQuestionDTOs.Add(new QuestionDTO { QuestionId = item.QuestionId, QuestionDisplay = item.Display, PossibleAnswers = possibleAnswersDTOs});
                possibleAnswers.Clear();
            }




            var myExamView = new ExaminationQuestionView()
            {
                Questions = selectedQuestionDTOs,
                CurrentQuestion = selectedQuestionDTOs.FirstOrDefault(),
            };



            return View(myExamView);

        }


        // POST: ExaminationViewController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> StartExamination(ExaminationView examinationView)
        {


            return View("Index");
        }

    }
}

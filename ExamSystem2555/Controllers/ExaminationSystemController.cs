using Microsoft.AspNetCore.Mvc;
using MyDatabase.Models;
using WebApp.DTO_Models.Final;
using WebApp.MainServices.Interfaces;

namespace WebApp.Controllers
{
    public class ExaminationSystemController : Controller
    {
        private readonly IExaminationManagerService _service;

        public ExaminationSystemController(IExaminationManagerService service)
        {
            _service = service;
        }
        public async Task<IActionResult> ExaminationSystemIndex(int? candidateExamId)
        {
            var model = await _service.CandidateExamService.GetCandidateExamByIdAsync(candidateExamId);
            return View(model);
        }


        public async Task<ActionResult> StartExamination(int candidateExamId)
        {
            var ce = await _service.CandidateExamService.GetCandidateExamByIdAsync(candidateExamId);
            await _service.CandidateExaminationLoad(ce);
            var myExam = await _service.ExaminationService.GetExaminationByIdAsync(ce.Examination.ExaminationId);


            var selectedExaminationQuestions = (await _service.ExaminationQuestionService.GetAllExaminationQuestionsAsync()).Where(e => e.Examination == myExam).ToList();
            await _service.ExaminationQuestionLoad(selectedExaminationQuestions);
            selectedExaminationQuestions.Select(x => x.CertificateTopicQuestion.TopicQuestion.Question).ToList();
            var selectedQuestions = new List<Question>();
            selectedExaminationQuestions.ForEach(x => selectedQuestions.Add(x.CertificateTopicQuestion.TopicQuestion.Question));
            var possibleAnswers = new List<QuestionPossibleAnswer>();
            var selectedQuestionDTOs = new List<QuestionDTO>();

            foreach (var item in selectedQuestions)
            {
                var myAnswers = (await _service.QuestionPossibleAnswerService.GetAllAnswersAsync()).Where(a => a.Question == item);
                possibleAnswers.AddRange(myAnswers);

                List<QuestionPossibleAnswersDTO> possibleAnswersDTOs = new List<QuestionPossibleAnswersDTO>();
                possibleAnswers.ForEach(x => possibleAnswersDTOs.Add(new QuestionPossibleAnswersDTO { QuestionPossibleAnswerId = x.QuestionPossibleAnswerId, QuestionPossibleAnswer = x.PossibleAnswer, IsAnswerCorrect = x.IsCorrect }));

                selectedQuestionDTOs.Add(new QuestionDTO { QuestionId = item.QuestionId, QuestionDisplay = item.Display, PossibleAnswers = possibleAnswersDTOs });
                possibleAnswers.Clear();


            }
            var myExamView = new ExaminationQuestionView()
            {
                Questions = selectedQuestionDTOs,
                CurrentIndex = 0,
                CandidateExamId = candidateExamId
            };

            return View(myExamView);

        }



        // POST: ExaminationViewController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> StartExamination([FromForm] ExaminationQuestionView myExamView, string action, int? SelectedAnswerId, string myModel)
        {

            var newModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ExaminationQuestionView>(myModel);

            if (action == "Next")
            {

                // Save the selected answer in the database
                if (SelectedAnswerId.HasValue)
                {
                    var ctqList = await _service.CertificateTopicQuestionService.GetAllCertificateTopicQuestionsAsync();
                    await _service.CertificateTopicsLoad(ctqList);
                    var myCertTopicQuestion = ctqList.First(x => x.TopicQuestion.Question.QuestionId == newModel.Questions[newModel.CurrentIndex].QuestionId);
                    var answers = new CandidateExaminationAnswer
                    {
                        CandidateExam = await _service.CandidateExamService.GetCandidateExamByIdAsync(newModel.CandidateExamId),
                        SelectedAnswer = (await _service.QuestionPossibleAnswerService.GetAnswerByIdAsync(SelectedAnswerId)).QuestionPossibleAnswerId,
                        CorrectAnswer = 1,
                        CertificateTopicQuestion = myCertTopicQuestion

                    };
                    await _service.ExamCandidateAnswerService.AddExamCandidateAnswerAsync(answers);
                    await _service.SaveChangesAsync();
                }
                newModel.CurrentIndex++;
                
                if (newModel.CurrentIndex >= newModel.Questions.Count)
                {
                    return RedirectToAction("GetResults", new { candidateExamId = newModel.CandidateExamId });
                }

            }

            return View(newModel);

        }
        public async Task<ActionResult> GetResults(int candidateExamId)
        {
            var x = await _service.ExamCandidateAnswerService.GetAllExamCandidateAnswersAsync();
            await _service.CandidateAnswerExamLoad(x);
            var examCandidateAnswer = x.Where(x => x.CandidateExam.CandidateExaminationId == candidateExamId);

            await _service.CandidateAnswerExamLoad(x);
            var totalQuestions = x.Count();
            var correctAnswers = 0;
            string result = "";
            foreach (var item in x)
            {
                if (item.SelectedAnswer == item.CorrectAnswer)
                {
                    correctAnswers++;
                }

            }

            if (correctAnswers > 2)
            {
                result = "PASS";
            }
            else
            {
                result = "FAIL";
            }

            var examResults = new CandidateExaminationResult
            {
                CandidateExaminationId = candidateExamId,
                ResultIssueDate = DateTime.Now,
                ResultLabel = result,
                CandidateTotalScore = correctAnswers

            };

            await _service.CandidateExamResultsService.AddCandidateExamResultsAsync(examResults);
            await _service.SaveChangesAsync();
            return View(examResults);
        }

    }
}

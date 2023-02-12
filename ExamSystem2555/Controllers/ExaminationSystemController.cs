using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyDatabase.Models;
using WebApp.DTO_Models._Questions;
using WebApp.DTO_Models.CandidateExaminations;
using WebApp.DTO_Models.Final;
using WebApp.MainServices.Interfaces;

namespace WebApp.Controllers
{
    public class ExaminationSystemController : Controller
    {
        private readonly IExaminationManagerService _service;
        private readonly IMapper _mapper;

        public ExaminationSystemController(IExaminationManagerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<IActionResult> ExaminationSystemIndex(int? candidateExamId)
        {
            var candidateExamination = await _service.CandidateExamService.GetCandidateExamByIdAsync(candidateExamId);
            await _service.CandidateExaminationLoad(candidateExamination);
            var model = _mapper.Map<CandidateExaminationsDTO>(candidateExamination);
            return View(model);
        }


        public async Task<ActionResult> StartExamination(int candidateExamId, int examId)
        {
            var myExam = await _service.ExaminationService.GetExaminationByIdAsync(examId);

            var selectedExaminationQuestions = (await _service.ExaminationQuestionService.GetAllExaminationQuestionsAsync()).Where(e => e.Examination == myExam).ToList();
            await _service.ExaminationQuestionLoad(selectedExaminationQuestions);

            var selectedQuestions = selectedExaminationQuestions.Select(x => x.CertificateTopicQuestion.TopicQuestion.Question);
            var selectedQuestionDTOs = _mapper.Map<List<NewQuestionDTO>>(selectedQuestions);

            var myExamView = new ExaminationQuestionView()
            {
                Questions = selectedQuestionDTOs,
                CurrentIndex = 0,
                CandidateExamId = candidateExamId,
                ExaminationId = examId
            };

            return View(myExamView);

        }

        // POST: ExaminationViewController/Create
        //[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Examination(int questionIndex, string action, int SelectedAnswerId, string myModel)
        {

            var newModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ExaminationQuestionView>(myModel);

            var myExam = await _service.ExaminationService.GetExaminationByIdAsync(newModel.ExaminationId);

            var candidateExamination = await _service.CandidateExamService.GetCandidateExamByIdAsync(newModel.CandidateExamId);

            var questionId = newModel.Questions[newModel.CurrentIndex].QuestionId;

            var question = await _service.QuestionService.GetQuestionByIdAsync(questionId);
            await _service.QuestionPossibleAnswersLoad(question);

            var questionCorrectAnswer = question.QuestionPossibleAnswers.Where(x => x.IsCorrect == true).FirstOrDefault();

            int selectedAnswerId = int.Parse(Request.Form["SelectedAnswerId"]);

            newModel.Questions[newModel.CurrentIndex].SelectedPossibleAnswerId = selectedAnswerId;

            foreach (var answer in newModel.Questions[newModel.CurrentIndex].QuestionPossibleAnswers)
            {
                if (answer.QuestionPossibleAnswerId == selectedAnswerId)
                {
                    answer.IsSelected = true;
                }
                else
                {
                    answer.IsSelected = false;
                }
            }

            var points = 0;

            if (SelectedAnswerId == questionCorrectAnswer.QuestionPossibleAnswerId)
            {
                points = question.Points;
            }

            var ctqList = await _service.CertificateTopicQuestionService.GetAllCertificateTopicQuestionsAsync();
            await _service.CertificateTopicsLoad(ctqList);
            var myCertTopicQuestion = ctqList.First(x => x.TopicQuestion.Question.QuestionId == questionId);

            var candidateExaminationAnswersList = (await _service.ExamCandidateAnswerService.GetAllExamCandidateAnswersAsync()).ToList();
            await _service.CandidateAnswerExamLoad(candidateExaminationAnswersList);

            var candidateExaminationAnswer = candidateExaminationAnswersList.FirstOrDefault(cea => cea.CandidateExam == candidateExamination && cea.CertificateTopicQuestion == myCertTopicQuestion);

            if (candidateExaminationAnswer == null)
            {
                var certificateExaminationAnswer = new CandidateExaminationAnswer
                {
                    CandidateExam = await _service.CandidateExamService.GetCandidateExamByIdAsync(newModel.CandidateExamId),
                    SelectedAnswer = (await _service.QuestionPossibleAnswerService.GetAnswerByIdAsync(SelectedAnswerId)).QuestionPossibleAnswerId,
                    CorrectAnswer = questionCorrectAnswer.QuestionPossibleAnswerId,
                    CertificateTopicQuestion = myCertTopicQuestion,
                    PointsAssignedDuringExamination = points

                };

                await _service.ExamCandidateAnswerService.AddExamCandidateAnswerAsync(certificateExaminationAnswer);
            }
            else
            {
                candidateExaminationAnswer.SelectedAnswer = (await _service.QuestionPossibleAnswerService.GetAnswerByIdAsync(SelectedAnswerId)).QuestionPossibleAnswerId;
                candidateExaminationAnswer.PointsAssignedDuringExamination = points;

                await _service.ExamCandidateAnswerService.UpdateExamCandidateAnswerAsync(candidateExaminationAnswer);

            }

            await _service.SaveChangesAsync();

            if (action == "SubmitExamination")
            {
                return RedirectToAction("GetResults", new { candidateExamId = newModel.CandidateExamId });
            }

            newModel.CurrentIndex = questionIndex;

            return View("StartExamination", newModel);

        }
        public async Task<ActionResult> GetResults(int candidateExamId)
        {
            var x = await _service.ExamCandidateAnswerService.GetAllExamCandidateAnswersAsync();
            await _service.CandidateAnswerExamLoad(x);
            var candidateExamAnswers = x.Where(x => x.CandidateExam.CandidateExaminationId == candidateExamId);

            await _service.CandidateAnswerExamLoad(x);
            var totalScore = 0;
            string result = "";

            foreach (var answer in candidateExamAnswers)
            {
                totalScore += answer.PointsAssignedDuringExamination;

            }

            if (totalScore > 2)
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
                CandidateTotalScore = totalScore,
                HasBeenRemarked = "IS NOT REMARKED"

            };

            await _service.CandidateExamResultsService.AddCandidateExamResultsAsync(examResults);
            await _service.SaveChangesAsync();
            return View(examResults);
        }

    }
}

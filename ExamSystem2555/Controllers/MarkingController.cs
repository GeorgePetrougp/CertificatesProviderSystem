using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Construction;
using MyDatabase.Models;
using System.Security.Claims;
using WebApp.DTO_Models.CandidateExaminations;
using WebApp.DTO_Models.Final;
using WebApp.DTO_Models.Marking;
using WebApp.MainServices;
using WebApp.MainServices.Interfaces;
using WebApp.Models;
using WebApp.DTO_Models.Questions;
using QuestionPossibleAnswersDTO = WebApp.DTO_Models.Final.QuestionPossibleAnswersDTO;

namespace WebApp.Controllers
{
    public class MarkingController : Controller
    {
        private readonly ICandidateExaminationManagerService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IMapper _mapper;

        public MarkingController(ICandidateExaminationManagerService service, IMapper mapper, UserManager<ApplicationUser> userManager)

        {
            _service = service;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: Marking
        public async Task<IActionResult> GetExams()
        {
            var x = User.Identity as ClaimsIdentity;
            var y = x.FindFirst(ClaimTypes.NameIdentifier);
            var z = y.Value;
            var user = await _userManager.FindByIdAsync(z);
            var exams = (await _service.MarkerAssignedExamService.GetAllMarkerAssignedExamsAsync()).Where(m => m.ApplicationUser == user).Select(ap => ap.CandidateExam);
            var candExam = new List<CandidateExamination>();
            foreach (var item in await _service.CandidateExamService.GetAllCandidateExamAsync())
            {
                foreach (var exam in exams)
                {
                    if (item == exam)
                    {
                        candExam.Add(item);
                    }
                }
            }

            return View(candExam);
        }



        public async Task<IActionResult> GetExamQuestions(int? id)
        {
            if (id == null || _service.CandidateExamService == null)
            {
                return NotFound();
            }
            var candidateExam = await _service.CandidateExamService.GetCandidateExamByIdAsync(id);
            var candidateAnswers = (await _service.CandidateAnswerService.GetAllExamCandidateAnswersAsync()).Where(a => a.CandidateExam == candidateExam);

            foreach (var answer in candidateAnswers)
            {
                await _service.CertificateTopicsQuestionLoad(answer);
            }

            if (candidateExam == null)
            {
                return NotFound();
            }

            return View(candidateAnswers);
        }


        public async Task<IActionResult> EditExamAnswers(int? id, int candidateExamId)
        {
            var candidateAnswer = await _service.CandidateAnswerService.GetExamCandidateAnswerByIdAsync(id);
            await _service.CertificateTopicsQuestionLoad(candidateAnswer);
            
            var question = candidateAnswer.CertificateTopicQuestion.TopicQuestion.Question;
            var questionPossibleAnswers = (candidateAnswer.CertificateTopicQuestion.TopicQuestion.Question.QuestionPossibleAnswers).ToList();

            var correctAnswerId = candidateAnswer.CorrectAnswer;
            var selectedAnswerId = candidateAnswer.SelectedAnswer;

            var model = new MarkingEditAnswerView
            {
                CandidateExaminationId = candidateExamId,
                CandidateAnswerId = id,
                Question = new DTO_Models.Final.QuestionDTO
                {

                    QuestionId = question.QuestionId,
                    PossibleAnswers = _mapper.Map<List<QuestionPossibleAnswersDTO>>(questionPossibleAnswers),
                    QuestionDisplay = question.Display

                },
                CorrectAnswer = correctAnswerId,
                SelectedAnswer = selectedAnswerId

            };

            foreach (var qpa in questionPossibleAnswers)
            {
                if (qpa.QuestionPossibleAnswerId == selectedAnswerId)
                {
                    model.IsSelectedAnswer= true;
                }
            }

            return View(model);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExamAnswers(int id, int candidateExamId, int selectedAnswer)
        {

            var candidateAnswer = await _service.CandidateAnswerService.GetExamCandidateAnswerByIdAsync(id);
            candidateAnswer.SelectedAnswer = selectedAnswer;
            await _service.SaveChangesAsync();

            var x = await _service.CandidateAnswerService.GetAllExamCandidateAnswersAsync();
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

            var examResults = (await _service.CandidateExamResultsService.GetAllCandidateExamResultsAsync()).Where(cer=>cer.CandidateExaminationId == candidateExamId).First();

            examResults.ResultLabel = result;
            examResults.CandidateTotalScore = correctAnswers;

            await _service.CandidateExamResultsService.UpdateCandidateExamResultsAsync(examResults);
            await _service.SaveChangesAsync();

            return RedirectToAction("GetExamQuestions", "Marking", new { id = candidateExamId });
        }

        private bool CandidateExamExists(int id)
        {
            return _service.CandidateExamService.GetCandidateExamByIdAsync(id) != null;
        }
    }
}

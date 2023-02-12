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
        public async Task<ActionResult> Examination(int questionIndex, ExaminationQuestionView myExamView, string action, int? SelectedAnswerId, string myModel)
        {

            var newModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ExaminationQuestionView>(myModel);

            //var myExam = await _service.ExaminationService.GetExaminationByIdAsync(myExamView.ExaminationId);
            var myExam = await _service.ExaminationService.GetExaminationByIdAsync(newModel.ExaminationId);

            //var candidateExamination = await _service.CandidateExamService.GetCandidateExamByIdAsync(myExamView.CandidateExamId);
            var candidateExamination = await _service.CandidateExamService.GetCandidateExamByIdAsync(newModel.CandidateExamId);

            //var questionId = myExamView.Questions[myExamView.CurrentIndex].QuestionId;
            var questionId = newModel.Questions[newModel.CurrentIndex].QuestionId;

            //var question = await _service.QuestionService.GetQuestionByIdAsync(myExamView.Questions[myExamView.CurrentIndex].QuestionId);
            var question = await _service.QuestionService.GetQuestionByIdAsync(questionId);

            await _service.QuestionPossibleAnswersLoad(question);

            var questionCorrectAnswer = question.QuestionPossibleAnswers.Where(x => x.IsCorrect == true).FirstOrDefault();


            foreach (var answer in newModel.Questions[newModel.CurrentIndex].QuestionPossibleAnswers)
            {
                if (answer.QuestionPossibleAnswerId == SelectedAnswerId)
                {
                    answer.IsSelected = true;
                    break;
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



            var candidateExaminationAnswers = await _service.ExamCandidateAnswerService.GetAllExamCandidateAnswersAsync();

            foreach (var cea in candidateExaminationAnswers)
            {
                if (cea.CandidateExam != candidateExamination || (cea.CandidateExam == candidateExamination && cea.CertificateTopicQuestion != myCertTopicQuestion))
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
                    cea.SelectedAnswer = (await _service.QuestionPossibleAnswerService.GetAnswerByIdAsync(newModel.SelectedAnswerId)).QuestionPossibleAnswerId;
                    cea.PointsAssignedDuringExamination = points;

                    await _service.ExamCandidateAnswerService.UpdateExamCandidateAnswerAsync(cea);
                }

            }

            //await _service.SaveChangesAsync();

            //var selectedExaminationQuestions = (await _service.ExaminationQuestionService.GetAllExaminationQuestionsAsync()).Where(e => e.Examination == myExam).ToList();
            //await _service.ExaminationQuestionLoad(selectedExaminationQuestions);

            //var selectedQuestions = selectedExaminationQuestions.Select(x => x.CertificateTopicQuestion.TopicQuestion.Question);
            //var selectedQuestionDTOs = _mapper.Map<List<NewQuestionDTO>>(selectedQuestions);

            //var newExamView = new ExaminationQuestionView()
            //{
            //    Questions = selectedQuestionDTOs,
            //    CurrentIndex = questionIndex,
            //    CandidateExamId = myExamView.CandidateExamId,
            //    ExaminationId = myExamView.ExaminationId

            //};
            //myExamView.Questions = selectedQuestionDTOs;
            newModel.CurrentIndex = questionIndex;


            return View("StartExamination",newModel);





















            //var newModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ExaminationQuestionView>(myModel);

            //var myQuestion = newModel.Questions[newModel.CurrentIndex];

            //var correctAnswer = myQuestion.QuestionPossibleAnswers.Where(x => x.IsCorrect == true).FirstOrDefault();


            //if (SelectedAnswerId == correctAnswer.QuestionPossibleAnswerId)
            //{
            //    points = myQuestion.Points;
            //}


            //if (action == "Next")
            //{

            //    // Save the selected answer in the database
            //    if (SelectedAnswerId.HasValue)
            //    {
            //        //var ctqList = await _service.CertificateTopicQuestionService.GetAllCertificateTopicQuestionsAsync();
            //        //await _service.CertificateTopicsLoad(ctqList);
            //        //var myCertTopicQuestion = ctqList.First(x => x.TopicQuestion.Question.QuestionId == newModel.Questions[newModel.CurrentIndex].QuestionId);



            //        var answers = new CandidateExaminationAnswer
            //        {
            //            CandidateExam = await _service.CandidateExamService.GetCandidateExamByIdAsync(newModel.CandidateExamId),
            //            SelectedAnswer = (await _service.QuestionPossibleAnswerService.GetAnswerByIdAsync(SelectedAnswerId)).QuestionPossibleAnswerId,
            //            CorrectAnswer = correctAnswer.QuestionPossibleAnswerId,
            //            CertificateTopicQuestion = myCertTopicQuestion,
            //            PointsAssignedDuringExamination = points

            //        };
            //        await _service.ExamCandidateAnswerService.AddExamCandidateAnswerAsync(answers);
            //        await _service.SaveChangesAsync();
            //    }
            //    newModel.CurrentIndex++;

            //    if (newModel.CurrentIndex >= newModel.Questions.Count)
            //    {
            //        return RedirectToAction("GetResults", new { candidateExamId = newModel.CandidateExamId });
            //    }

            //}

            //return View(newModel);

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

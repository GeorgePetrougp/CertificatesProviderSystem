using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDatabase.Models;
using Newtonsoft.Json;
using System;
using WebApp.DTO_Models.Final;
using WebApp.MainServices;

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
        public async Task<ActionResult> Index(int candidateExamId)
        {
            var myExam = await _service.CandidateExamService.GetCandidateExamByIdAsync(candidateExamId);

            return View(myExam);
        }

        // GET: ExaminationViewController/Create
        public async Task<ActionResult> StartExamination(CandidateExam mmyExam)
        {
            var myExam = await _service.ExaminationService.GetExaminationByIdAsync(1);


            var selectedExaminationQuestions = (await _service.ExamQuestionService.GetAllExaminationQuestionsAsync()).Where(e => e.Examination == myExam).ToList();
            await _service.ExaminationQuestionLoad(selectedExaminationQuestions);
            selectedExaminationQuestions.Select(x => x.CertificateTopicQuestion.TopicQuestion.Question).ToList();
            var selectedQuestions = new List<Question>();
            selectedExaminationQuestions.ForEach(x => selectedQuestions.Add(x.CertificateTopicQuestion.TopicQuestion.Question));
            var possibleAnswers = new List<QuestionPossibleAnswer>();
            var selectedQuestionDTOs = new QuestionDTO[selectedQuestions.Count];

            var h = 0;
            foreach (var item in selectedQuestions)
            {
                var myAnswers = (await _service.AnswerService.GetAllAnswersAsync()).Where(a => a.Question == item);
                possibleAnswers.AddRange(myAnswers);

                List<QuestionPossibleAnswersDTO> possibleAnswersDTOs = new List<QuestionPossibleAnswersDTO>();
                possibleAnswers.ForEach(x => possibleAnswersDTOs.Add(new QuestionPossibleAnswersDTO { QuestionPossibleAnswerId = x.QuestionPossibleAnswerId, QuestionPossibleAnswer = x.PossibleAnswer, IsAnswerCorrect = x.IsCorrect }));

               
                    selectedQuestionDTOs[h] = new QuestionDTO
                    {
                        QuestionId = item.QuestionId,
                        QuestionDisplay = item.Display,
                        PossibleAnswers = possibleAnswersDTOs
                    };
                
                possibleAnswers.Clear();
                h++;


            }
                var myExamView = new ExaminationQuestionView()
                {
                    Questions = selectedQuestionDTOs,
                    CurrentIndex = 0
                };



                return View(myExamView);

            }
        


        // POST: ExaminationViewController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> StartExamination([FromForm]ExaminationQuestionView myExamView, string action, int? SelectedAnswerId)
        {
            if (action == "Next")
                {
                myExamView.CurrentIndex++;
                    if (myExamView.CurrentIndex >= myExamView.Questions.Length)
                    {
                        return RedirectToAction("ExamFinished");
                    }

                    // Save the selected answer in the database
                    if (SelectedAnswerId.HasValue)
                    {
                        var ctq = (await _service.CertificateTopicQuestionService.GetAllCertificateTopicQuestionsAsync()).First(x => x.TopicQuestion.Question.QuestionId == myExamView.Questions[myExamView.CurrentIndex].QuestionId);
                        await _service.CertificateTopicsLoad(ctq);
                        var answers = new ExamCandidateAnswer
                        {
                            CandidateExam = await _service.CandidateExamService.GetCandidateExamByIdAsync(myExamView.CandidateExamId),
                            SelectedAnswer = (await _service.AnswerService.GetAnswerByIdAsync(SelectedAnswerId)).PossibleAnswer,
                            CorrectAnswer = "correct",
                            CertificateTopicQuestion = ctq


                        };
                        await _service.CandidateAnswerService.AddExamCandidateAnswerAsync(answers);
                        await _service.SaveChangesAsync();
                    }

                }

                return View(myExamView);
            
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using WebApp.Data;
using WebApp.DTO_Models.Final;
using WebApp.MainServices;

namespace WebApp.Controllers
{
    public class MarkingController : Controller
    {
        private readonly IExamManagerService _service;
        private readonly IMapper _mapper;

        public MarkingController(IExamManagerService service, IMapper mapper)
        {
            _service= service;
            _mapper = mapper;
        }

        // GET: Marking
        public async Task<IActionResult> GetExams()
        {
              return View(await _service.CandidateExamService.GetAllCandidateExamAsync());
        }

        

        public async Task<IActionResult> GetExamQuestions(int? id)
        {
            if (id == null || _service.CandidateExamService == null)
            {
                return NotFound();
            }
            var candidateExam = await _service.CandidateExamService.GetCandidateExamByIdAsync(id);
            var examDetails = (await _service.CandidateAnswerService.GetAllExamCandidateAnswersAsync()).Where(a=>a.CandidateExam == candidateExam);

            foreach (var item in examDetails)
            {
                await _service.CertificateTopicsQuestionLoad(item);
            }



            if (candidateExam == null)
            {
                return NotFound();
            }
            return View(examDetails);
        }


        public async Task<IActionResult> EditExamAnswers(int? id)
        {
            var myExamAnswer = await _service.CandidateAnswerService.GetExamCandidateAnswerByIdAsync(id);
            await _service.CertificateTopicsQuestionLoad(myExamAnswer);
            var answers = (myExamAnswer.CertificateTopicQuestion.TopicQuestion.Question.QuestionPossibleAnswers).ToList();
            var question = myExamAnswer.CertificateTopicQuestion.TopicQuestion.Question;
            var correctAnswer = myExamAnswer.CorrectAnswer;
            var selectedAnswer = myExamAnswer.SelectedAnswer;


            var model = new MarkingEditAnswerView
            {
                CandidateAnswerId =id ,
                 Question = new QuestionDTO
                 {
                     
                     QuestionId=question.QuestionId,
                     PossibleAnswers=_mapper.Map<List<QuestionPossibleAnswersDTO>>(answers),
                     QuestionDisplay=question.Display
                     
                 },
                 CorrectAnswer = correctAnswer,
                 SelectedAnswer = selectedAnswer,

            };
            return View(model);

        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExamAnswers(int id,int selectedAnswer)
        {

            var candidateAnswer = await _service.CandidateAnswerService.GetExamCandidateAnswerByIdAsync(id);
            candidateAnswer.SelectedAnswer = selectedAnswer;
            await _service.SaveChangesAsync();
            
            return RedirectToAction("GetExamQuestions", "Marking", new {id=id});
        }

        // GET: Marking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _service.CandidateExamService == null)
            {
                return NotFound();
            }

            var candidateExam = await _service.CandidateExamService.GetCandidateExamByIdAsync(id);
            if (candidateExam == null)
            {
                return NotFound();
            }

            return View(candidateExam);
        }

        // POST: Marking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_service.CandidateExamService == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CandidateExams'  is null.");
            }
            var candidateExam = await _service.CandidateExamService.GetCandidateExamByIdAsync(id);
            if (candidateExam != null)
            {
                await _service.CandidateExamService.DeleteCandidateExamAsync(id);
            }
            
            await _service.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidateExamExists(int id)
        {
          return _service.CandidateExamService.GetCandidateExamByIdAsync(id) != null;
        }
    }
}
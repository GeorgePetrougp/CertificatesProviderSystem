using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using NuGet.Packaging;
using System.Runtime.InteropServices;
using WebApp.DTO_Models;
using WebApp.MainServices;

namespace WebApp.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly IQuestionManagerService _service;
        private readonly IMapper _mapper;


        public QuestionsController(IQuestionManagerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            return View(await _service.QuestionService.GetAllQuestionsAsync());
        }

        // GET: Questions/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _service.QuestionService == null)
            {
                return NotFound();
            }

            var question = await _service.QuestionService.GetQuestionByIdAsync(id);

            if (question == null)
            {
                return NotFound();
            }
            await _service.QuestionLoad(question);
            var details = new QuestionDetailsView
            {
                QuestionDetailsViewId = question.QuestionId,
                QuestionDisplay = question.Display,
                QuestionDifficulty = question.QuestionDifficulty.Difficulty,
                Answers = _service.AnswerService.GetAllAnswersAsync().Result.Where(an => an.Question == question).ToList(),
                Topics = new List<Topic>()
            };
            var topicQuestion = _service.TopicQuestionService.GetAllTopicQuestionsAsync().Result.Where(tq => tq.Question == question).ToList();
            // topiQuestion where question with id 1 exists

            foreach (var item in topicQuestion)
            {
                await _service.TopicQuestionLoad(item);
                if(item.Topic != null)
                {
                    details.Topics.Add(item.Topic);
                }
                
            }

            // from certificatetopicquestions 

            details.Certificates = new List<Certificate>();
            var certificateTopicQuestion = _service.CertificateTopicQuestionService.GetAllCertificateTopicQuestionsAsync().Result.ToList();
            foreach (var item in topicQuestion)
            {
                var certTopicQuestList =(await _service.CertificateTopicQuestionService.GetAllCertificateTopicQuestionsAsync()).Where(ctq => ctq.TopicQuestion == item);
                foreach (var cert in certTopicQuestList)
                {
                        await _service.CertificateTopicsLoad(cert);
                    
                        details.Certificates.Add(cert.CertificateTopic.Certificate);
                    
                }
            }
            var x = details.Certificates.GroupBy(c=>c.CertificateId).Select(x=>x.First()).ToList();
            details.Certificates = x;

            return View(details);
        }

        // GET: Questions/Create
        public async Task<IActionResult> Create()
        {
            var topicsList = await _service.TopicService.GetAllTopicsAsync();
            var certificateList = await _service.CertificateService.GetAllCertificatesAsync();
            var difficultiesList = await _service.QuestionDifficultyService.GetAllDifficultiesAsync();
            var newQuestion = new QuestionView();
            newQuestion.Difficulty.Difficulties = new SelectList(difficultiesList, "QuestionDifficultyId", "Difficulty");
            newQuestion.CertificatesView.CertificateList = new MultiSelectList(certificateList, "CertificateId", "Title");
            newQuestion.TopicView.TopicsList = new MultiSelectList(topicsList, "TopicId", "Title");

            return View(newQuestion);
        }


        // POST: Questions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] QuestionView question)
        {
            if (ModelState.IsValid)
            {
                //SqlException: Cannot insert explicit value for identity column in table 'QuestionDifficulties' when IDENTITY_INSERT is set 
                var newQuestion = _mapper.Map<Question>(question);
                //newQuestion.QuestionDifficulty.QuestionDifficultyId = question.Difficulty.SelectedId;
                newQuestion.QuestionDifficulty = await _service.QuestionDifficultyService.GetDifficultyByIdAsync(question.Difficulty.SelectedId);
                newQuestion.QuestionPossibleAnswers = _mapper.Map<List<QuestionPossibleAnswer>>(question.AnswerViews);
                //await _service.QuestionService.AddQuestionAsync(newQuestion);




                var topicIds = question.TopicView.SelectedTopicIds;
                var certificatesId = question.CertificatesView.SelectedCertificateIds;


                if (topicIds == null)
                {
                    var newTopicQuestion = new TopicQuestion { Question = newQuestion, Topic = null };

                    var selectedCertificates = (await _service.CertificateService.SortCertificatesById(certificatesId)).ToList();
                    selectedCertificates.ForEach(sc => _service.CertificateTopicQuestionService.AddCertificateTopicQuestionAsync(new CertificateTopic { Certificate = sc, Topic = null }, newTopicQuestion));
                }
                else
                {
                    var selectedTopics = (await _service.TopicService.GetAllTopicsAsync()).Where(ti => topicIds.Contains(ti.TopicId)).ToList();
                    var newTopicQuestionList = selectedTopics.Select(t => new TopicQuestion { Topic = t, Question = newQuestion });
                    

                    foreach (var tq in newTopicQuestionList)
                    {
                        foreach (var ct in (await _service.CertificateTopicService.GetAllCertificateTopicsAsync()).ToList())
                        {
                            if (ct.Topic == tq.Topic)
                            {
                                await _service.CertificateTopicQuestionService.AddCertificateTopicQuestionAsync(ct, tq);
                            }
                        }
                    }
                }
                await _service.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(question);
        }


        // GET: Questions/Edit
        public async Task<IActionResult> Edit(int? id)
        {

            
            if (id == null || _service.QuestionService == null)
            {
                return NotFound();
            }

            var question = await _service.QuestionService.GetQuestionByIdAsync(id);
            var difficulties = await _service.QuestionDifficultyService.GetAllDifficultiesAsync();
            var topics = await _service.TopicService.GetAllTopicsAsync();
            var certificates = await _service.CertificateService.GetAllCertificatesAsync();
            await _service.QuestionLoad(question);

            EditQuestionView editModel = new EditQuestionView()
            {
                EditQuestionId = question.QuestionId,
                EditQuestionDisplay = question.Display,
                 EditDifficultyLevel = question.QuestionDifficulty.Difficulty

            };
            editModel.EditDifficulty.Difficulties = new SelectList(difficulties, "QuestionDifficultyId", "Difficulty");
            editModel.TopicsList = new MultiSelectList(topics, "TopicId", "Title");
            editModel.CertificateList = new MultiSelectList(certificates, "CertificateId", "Title");
            var currentAnswers = (await _service.AnswerService.GetAllAnswersAsync()).Where(ca => ca.Question == question).ToList();

            foreach (var item in currentAnswers)
            {
                editModel.AnswerViews.Add(
                    
                    new AnswerView
                    {
                        QAnswerId = item.Question.QuestionId,
                        Answer = item.PossibleAnswer,
                        IsCorrect = item.IsCorrect
                    });       
            }
            var currentTopicQuestions = (await _service.TopicQuestionService.GetAllTopicQuestionsAsync()).Where(tq => tq.Question == question);
            
            foreach (var topicQuestion in currentTopicQuestions)
            {
                var currentTopic = (await _service.TopicService.GetAllTopicsAsync()).Where(t => t.TopicId == topicQuestion.Topic.TopicId);
                editModel.Topics.AddRange(currentTopic);

            }

            var certificateTopicQuestions = await _service.CertificateTopicQuestionService.GetAllCertificateTopicQuestionsAsync();

            foreach(var certificateTopicQuestion in certificateTopicQuestions)
            {
                foreach(var topicQuestion in currentTopicQuestions)
                {
                    if(certificateTopicQuestion.TopicQuestion == topicQuestion)
                    {
                        await _service.CertificateTopicsLoad(certificateTopicQuestion);
                        var currentCqertificate = (await _service.CertificateService.GetAllCertificatesAsync()).Where(t => t.CertificateId == certificateTopicQuestion.CertificateTopic.Certificate.CertificateId);
                        editModel.Certificates.AddRange(currentCqertificate);
                    }
                }
            }
  
            if (question == null)
            {
                return NotFound();
            }
            return View(editModel);
        }

        // POST: Questions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] EditQuestionView question)
        {
            var newQuestion =await _service.QuestionService.GetQuestionByIdAsync(question.EditQuestionId);
            newQuestion = _mapper.Map<Question>(question);
            newQuestion.QuestionDifficulty = await _service.QuestionDifficultyService.GetDifficultyByIdAsync(question.EditDifficulty.SelectedId);
            //newQuestion.QuestionPossibleAnswers = _mapper.Map<List<QuestionPossibleAnswer>>(question.AnswerViews);
            //foreach (var item in question.AnswerViews)
            //{
            //    newQuestion.QuestionPossibleAnswers;
            //}

            var topicIds = question.TopicIds;
            var certificatesId = question.CertificateIds;

            if (id != newQuestion.QuestionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.QuestionService.UpdateQuestionAsync(newQuestion);

                    //if (topicIds == null)
                    //{
                    //    var newTopicQuestion = new TopicQuestion { Question = newQuestion, Topic = null };

                    //    var selectedCertificates = (await _service.CertificateService.SortCertificatesById(certificatesId)).ToList();
                    //    selectedCertificates.ForEach(sc => _service.CertificateTopicQuestionService.UpdateCertificateTopicQuestionAsync(new CertificateTopic { Certificate = sc, Topic = null }, newTopicQuestion));
                    //}
                    //else
                    //{
                    //    var selectedTopics = (await _service.TopicService.GetAllTopicsAsync()).Where(ti => topicIds.Contains(ti.TopicId)).ToList();
                    //    var newTopicQuestionList = selectedTopics.Select(t => new TopicQuestion { Topic = t, Question = newQuestion });


                    //    foreach (var tq in newTopicQuestionList)
                    //    {
                    //        foreach (var ct in (await _service.CertificateTopicService.GetAllCertificateTopicsAsync()).ToList())
                    //        {
                    //            if (ct.Topic == tq.Topic)
                    //            {
                    //                await _service.CertificateTopicQuestionService.AddCertificateTopicQuestionAsync(ct, tq);
                    //            }
                    //        }
                    //    }
                    //}







                    await _service.SaveChanges();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(newQuestion.QuestionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _service.QuestionService == null)
            {
                return NotFound();
            }

            var question = await _service.QuestionService.GetQuestionByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_service.QuestionService == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Questions'  is null.");
            }
            var question = await _service.QuestionService.GetQuestionByIdAsync(id);
            if (question != null)
            {
                await _service.QuestionService.DeleteQuestionAsync(id);
            }

            await _service.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _service.QuestionService.GetQuestionByIdAsync(id) != null;
        }
    }
}

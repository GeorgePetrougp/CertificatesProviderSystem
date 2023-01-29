using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
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
                if (item.Topic != null)
                {
                    details.Topics.Add(item.Topic);
                }

            }

            // from certificatetopicquestions 

            details.Certificates = new List<Certificate>();
            var certificateTopicQuestion = _service.CertificateTopicQuestionService.GetAllCertificateTopicQuestionsAsync().Result.ToList();
            foreach (var item in topicQuestion)
            {
                var certTopicQuestList = (await _service.CertificateTopicQuestionService.GetAllCertificateTopicQuestionsAsync()).Where(ctq => ctq.TopicQuestion == item);
                foreach (var cert in certTopicQuestList)
                {
                    await _service.CertificateTopicsLoad(cert);

                    details.Certificates.Add(cert.CertificateTopic.Certificate);

                }
            }
            var x = details.Certificates.GroupBy(c => c.CertificateId).Select(x => x.First()).ToList();
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

            foreach (var certificateTopicQuestion in certificateTopicQuestions)
            {
                foreach (var topicQuestion in currentTopicQuestions)
                {
                    if (certificateTopicQuestion.TopicQuestion == topicQuestion)
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
            var newQuestion = await _service.QuestionService.GetQuestionByIdAsync(question.EditQuestionId);
            newQuestion = _mapper.Map<Question>(question);
            newQuestion.QuestionDifficulty = await _service.QuestionDifficultyService.GetDifficultyByIdAsync(question.EditDifficulty.SelectedId);
 
            if (id != newQuestion.QuestionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

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
                return RedirectToAction("EditTopicsAndCertificates", "Questions", new { id = newQuestion.QuestionId });
            }
            return View(question);
        }



        //Extra Edits For Question(Topics,Certificates)

        public async Task<IActionResult> EditTopicsAndCertificates(int? id)
        {

            List<Topic> topicsList = new List<Topic>();
            List<Certificate> certificateList = new List<Certificate>();

            var allTopicQuestions = await _service.TopicQuestionService.GetAllTopicQuestionsAsync();
            foreach (var tq in allTopicQuestions)
            {
                await _service.TopicQuestionLoad(tq);
            }
            var topicQuestions = allTopicQuestions.Where(tq => tq.Question.QuestionId == id);
            foreach (var topicQuestion in topicQuestions)
            {
                var certificateTopicQuestions = (await _service.CertificateTopicQuestionService.GetAllCertificateTopicQuestionsAsync()).Where(ctq => ctq.TopicQuestion == topicQuestion);
                foreach (var certificateTopicQuestion in certificateTopicQuestions)
                {
                    await _service.CertificateTopicsLoad(certificateTopicQuestion);
                    topicsList.Add(certificateTopicQuestion.CertificateTopic.Topic);
                    certificateList.Add(certificateTopicQuestion.CertificateTopic.Certificate);

                }
            }
            EditCertAndTopicsView model = new EditCertAndTopicsView()
            {
                CurrentCertificateList = certificateList,
                CurrentTopicsList = topicsList
            };


            var topics = await _service.TopicService.GetAllTopicsAsync();
            var certificates = await _service.CertificateService.GetAllCertificatesAsync();
            model.TopicsList = new MultiSelectList(topics, "TopicId", "Title");
            model.CertificateList = new MultiSelectList(certificates, "CertificateId", "Title");

            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTopicsAndCertificates(int id, [FromForm] EditCertAndTopicsView model)
        {
            var question = await _service.QuestionService.GetQuestionByIdAsync(id);
            var topicQuestionsNoTopic = (await _service.TopicQuestionService.GetAllTopicQuestionsAsync()).Where(tq => tq.Question == question && tq.Topic == null).First();

            var selectedCertificateIds = model.SelectedCertificates;

            List<Certificate> certificates = new List<Certificate>();

            foreach (var certificateId in selectedCertificateIds)
            {
                certificates.Add((await _service.CertificateService.GetAllCertificatesAsync()).Where(c => c.CertificateId == certificateId).SingleOrDefault());
            }

            foreach (var certificate in certificates)
            {
                var newCertificateTopic = new CertificateTopic()
                {
                    Certificate = certificate,
                    Topic = null
                };
                await _service.CertificateTopicQuestionService.AddCertificateTopicQuestionAsync(newCertificateTopic, topicQuestionsNoTopic);
            }

            await _service.SaveChanges();

            return RedirectToAction(nameof(Index));

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


        public async Task<IActionResult> EditQuestionTopicsIndex(int? id)
        {
            EditQuestionTopicView model = new EditQuestionTopicView();
            List<Topic> topics = new List<Topic>();

            var allTopicQuestions = await _service.TopicQuestionService.GetAllTopicQuestionsAsync();
            foreach (var tq in allTopicQuestions)
            {
                await _service.TopicQuestionLoad(tq);
            }
            var topicQuestions = allTopicQuestions.Where(tq => tq.Question.QuestionId == id);
            foreach (var topicQuestion in topicQuestions)
            {
                topics.Add(topicQuestion.Topic);
            }
            model.Topics = topics;
            model.QuestionId = id;

            return View(model);
        }

        public async Task<IActionResult> AddQuestionTopic(EditQuestionTopicView model)
        {
            var topics = await _service.TopicService.GetAllTopicsAsync();
            model.TopicsList = new MultiSelectList(topics, "TopicId", "Title");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AddQuestionTopic(int? id, [FromForm] EditQuestionTopicView model)
        {

            var question = await _service.QuestionService.GetQuestionByIdAsync(id);
            var selectedTopics = model.SelectedTopicIds.ToList();
            List<Topic> sortedTopics = new List<Topic>();

            var allTopics = await _service.TopicService.GetAllTopicsAsync();
            foreach (var item in allTopics)
            {

                foreach (var topic in selectedTopics)
                {
                    if (topic == item.TopicId)
                    {
                        sortedTopics.Add(item);
                    }
                }
            }
            var certificateTopics = await _service.CertificateTopicService.GetAllCertificateTopicsAsync();
            foreach (var cert in certificateTopics)
            {

                foreach (var topic in sortedTopics)
                {
                    if (topic == cert.Topic)
                    {
                        var certTopic = await _service.CertificateTopicService.GetCertificateTopicByIdAsync(cert.CertificateTopicId);
                        await _service.CertificateTopicQuestionService.AddCertificateTopicQuestionAsync(certTopic, new TopicQuestion { Topic = topic, Question = question });
                    }

                }
            }

            await _service.SaveChanges();

            return RedirectToAction("EditQuestionTopicsIndex", "Questions", new { id = id });



        }
        //################################################################################################################


        public async Task<IActionResult> EditQuestionCertificatesIndex(int? id)
        {
            EditQuestionCertificateView model = new EditQuestionCertificateView();
            List<Certificate> certificates = new List<Certificate>();

            var allCertificateTopicQuestions = await _service.CertificateTopicQuestionService.GetAllCertificateTopicQuestionsAsync();


            var allTopicQuestions = await _service.TopicQuestionService.GetAllTopicQuestionsAsync();
            foreach (var tq in allTopicQuestions)
            {
                await _service.TopicQuestionLoad(tq);
            }
            var topicQuestions = allTopicQuestions.Where(tq => tq.Question.QuestionId == id);

            foreach (var certTopicQuest in allCertificateTopicQuestions)
            {
                foreach (var topicQuestion in topicQuestions)
                {
                    if (certTopicQuest.TopicQuestion == topicQuestion)
                    {
                        await _service.CertificateTopicsLoad(certTopicQuest);
                        certificates.Add(certTopicQuest.CertificateTopic.Certificate);
                    }
                }
            }


            model.Certificates = certificates;
            model.QuestionId = id;

            return View(model);
        }

        public async Task<IActionResult> AddQuestionCertificate(EditQuestionCertificateView model)
        {
            var certificates = await _service.CertificateService.GetAllCertificatesAsync();
            model.CertificatesList = new MultiSelectList(certificates, "CertificateId", "Title");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AddQuestionCertificate(int? id, [FromForm] EditQuestionCertificateView model)
        {

            var question = await _service.QuestionService.GetQuestionByIdAsync(id);
            var selectedCertificates = model.SelectedCertificateIds.ToList();
            List<Certificate> sortedCertificates = new List<Certificate>();

            var allCertificates = await _service.CertificateService.GetAllCertificatesAsync();

            selectedCertificates.ForEach(async x => sortedCertificates.Add(await _service.CertificateService.GetCertificateByIdAsync(x)));


            var allCertificateTopics = await _service.CertificateTopicService.GetAllCertificateTopicsAsync();
            List<CertificateTopic> certificateTopicsList = new List<CertificateTopic>();
            foreach (var certificate in sortedCertificates)
            {

                foreach (var certTopic in allCertificateTopics)
                {

                    if (certTopic.Certificate == certificate)
                    {
                        certificateTopicsList.Add(certTopic);
                    }
                }
            }

            var allTopicQuestions = await _service.TopicQuestionService.GetAllTopicQuestionsAsync();
            TopicQuestion myNewTopicQuestion = new TopicQuestion();


            foreach (var topicQuestion in allTopicQuestions)
            {
                if (topicQuestion.Topic == null)
                {
                    myNewTopicQuestion = topicQuestion;
                }
            }

            foreach (var sortedCertificate in sortedCertificates)
            {

                if (certificateTopicsList.Count == 0)
                {

                        await _service.CertificateTopicQuestionService.AddCertificateTopicQuestionAsync(new CertificateTopic { Certificate = sortedCertificate, Topic = null }, myNewTopicQuestion);

                }
                else
                {
                    foreach (var certificateTopic in certificateTopicsList)
                    {
                    await _service.CertificateTopicQuestionService.AddCertificateTopicQuestionAsync(certificateTopic, myNewTopicQuestion);

                    }
                }
            }



            await _service.SaveChanges();

            return RedirectToAction("EditQuestionCertificatesIndex", "Questions", new { id = id });



        }



    }
}

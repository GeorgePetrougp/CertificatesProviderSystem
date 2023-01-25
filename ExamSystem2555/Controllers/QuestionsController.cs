using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
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

        // GET: Questions/Details/5
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
            QuestionDetailsView details = new QuestionDetailsView();

            details.QuestionDetailsViewId = question.QuestionId;
            details.QuestionDisplay = question.Display;
            details.QuestionDifficulty = question.QuestionDifficulty.Difficulty;
            details.Answers = _service.AnswerService.GetAllAnswersAsync().Result.Where(an => an.Question == question).ToList();

            details.Topics = new List<Topic>();
            var topicQuestion = _service.TopicQuestionService.GetAllTopicQuestionsAsync().Result.Where(tq => tq.Question == question).ToList();

            foreach (var item in topicQuestion)
            {
                await _service.TopicQuestionLoad(item);

                details.Topics.Add(item.Topic);
            }

            // from certificatetopicquestions 

            details.Certificates = new List<Certificate>();
            var certificateTopicQuestion = _service.CertificateTopicQuestionService.GetAllCertificateTopicQuestionsAsync().Result.ToList();
            foreach (var item in topicQuestion)
            {
                var certTopicQuestList = _service.CertificateTopicQuestionService.GetAllCertificateTopicQuestionsAsync().Result.Where(ctq => ctq.TopicQuestion == item);
                foreach (var cert in certificateTopicQuestion)
                {
                    if (cert.TopicQuestion == item)
                    {
                        await _service.CertificateQuestionLoad(cert);
                        details.Certificates.Add(cert.CertificateTopic.Certificate);
                    }
                }
            }





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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] QuestionView question)
        {
            if (ModelState.IsValid)
            {
                //var newQuestion = await _service.CreateNewQuestion(question);
                //SqlException: Cannot insert explicit value for identity column in table 'QuestionDifficulties' when IDENTITY_INSERT is set to OFF.
                var myQuestion = _mapper.Map<Question>(question);

                //Adding Question and QuestionDifficulty
                //myQuestion.QuestionDifficulty = await _service.QuestionDifficultyService.GetDifficultyByIdAsync(question.Difficulty.SelectedId);
                //myQuestion.QuestionPossibleAnswers = _mapper.Map<List<QuestionPossibleAnswer>>(question.AnswerViews);
                //await _service.QuestionService.AddQuestionAsync(myQuestion);

                //Adding Topics,Certificates
                myQuestion.TopicQuestions = new List<TopicQuestion>();
                if (question.TopicView.SelectedTopicIds == null)
                {
                    var myTopicQuestion = new TopicQuestion
                    {
                        Question = myQuestion,
                        Topic = null
                    };
                    await _service.TopicQuestionService.AddTopicQuestionAsync(myTopicQuestion);



                }
                else
                {

                    var topicIds = question.TopicView.SelectedTopicIds.ToList();
                    foreach (var topicId in topicIds)
                    {
                        var myTopicQuestion = new TopicQuestion
                        {
                            Question = myQuestion,
                            Topic = await _service.TopicService.GetTopicByIdAsync(topicId)
                        };
                        await _service.TopicQuestionService.AddTopicQuestionAsync(myTopicQuestion);
                    }

                    //IEnumerable<CertificateTopic> allCertTopics = await _service.CertificateTopicService.GetAllCertificateTopicsAsync();
                    //List<CertificateTopic> filteredList = new List<CertificateTopic>();

                    //foreach (var topicId in topicIds)
                    //{
                    //    foreach (var item in allCertTopics)
                    //    {
                    //        if (topicId == item.Topic.TopicId)
                    //        {
                    //            filteredList.Add(item);
                    //        }
                    //    }

                    //}

                    //foreach (var item in filteredList)
                    //{
                    //    var newFanantziofDeath = new CertificateTopicQuestion
                    //    {

                    //    };



                    //}


                    //var finalList = filteredList.Distinct();



                }


                await _service.SaveChanges();


                return RedirectToAction(nameof(Index));
            }

            return View(question);
        }


        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestionId,Display")] Question question)
        {
            if (id != question.QuestionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.QuestionService.UpdateQuestionAsync(question);
                    await _service.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.QuestionId))
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

        // POST: Questions/Delete/5
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

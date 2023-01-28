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
            var details = new QuestionDetailsView
            {
                QuestionDetailsViewId = question.QuestionId,
                QuestionDisplay = question.Display,
                QuestionDifficulty = question.QuestionDifficulty.Difficulty,
                Answers = _service.AnswerService.GetAllAnswersAsync().Result.Where(an => an.Question == question).ToList(),
                Topics = new List<Topic>()
            };
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
                //SqlException: Cannot insert explicit value for identity column in table 'QuestionDifficulties' when IDENTITY_INSERT is set to OFF.
                var newQuestion = _mapper.Map<Question>(question);

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

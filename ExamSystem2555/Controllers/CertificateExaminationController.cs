using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyDatabase.Models;
using Newtonsoft.Json;
using System.Data;
using WebApp.DTO_Models.CertificateExaminations;
using WebApp.DTO_Models.Final;
using WebApp.MainServices.Interfaces;
using WebApp.Models;


namespace WebApp.Controllers
{
    public class CertificateExaminationController : Controller
    {
        private readonly ICertificateExaminationService _service;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public CertificateExaminationController(IMapper mapper,ICertificateExaminationService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
            _mapper = mapper;
        }
        [Authorize(Roles = "Quality Controller,Administrator")]
        public async Task<IActionResult> CertificateExaminationsIndex()
        {
            var exams = (await _service.ExaminationService.GetAllExaminationsAsync()).ToList();
            var examsDTOList = new List<CertificateExaminationView>();

            foreach (var exam in exams)
            {
                await _service.LoadCertificates(exam);
                examsDTOList.Add(new CertificateExaminationView
                {
                    ExaminationId = exam.ExaminationId,
                    CertificateId = exam.Certificate.CertificateId,
                    CertificateTitle = exam.Certificate.Title
                });

            }

            return View(examsDTOList);
        }

        [Authorize(Roles = "Quality Controller,Administrator")]
        public async Task<IActionResult> CertificateExaminationDetails(int id)
        {
            var examination = await _service.ExaminationService.GetExaminationByIdAsync(id);
            await _service.LoadCertificates(examination);

            var examCtqList = (await _service.ExaminationQuestionService.GetAllExaminationQuestionsAsync()).Where(eq => eq.Examination == examination).ToList();

            var questions = new List<CertificateExaminationQuestionDTO>();

            foreach (var item in examCtqList)
            {
                await _service.LoadCTQ(item);
                questions.Add(new CertificateExaminationQuestionDTO
                {
                    QuestionId = item.CertificateTopicQuestion.TopicQuestion.Question.QuestionId,
                    QuestionDisplay = item.CertificateTopicQuestion.TopicQuestion.Question.Display
                });
            }

            var examDTO = new CertificateExaminationView
            {
                ExaminationId = examination.ExaminationId,
                CertificateId = examination.Certificate.CertificateId,
                CertificateTitle = examination.Certificate.Title,
                Questions = questions

            };

            return View(examDTO);

        }

        public async Task<IActionResult> SelectCertificateForExamination()
        {
            var certificates = await _service.CertificateService.GetAllCertificatesAsync();
            var model = new ExaminationDTO
            {
                CertificatesList = new SelectList(certificates, "CertificateId", "Title")
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SelectQuestionsForExamination(int selectedCertificateId)
        {
            //get certificate by id
            var certificate = await _service.CertificateService.GetCertificateByIdAsync(selectedCertificateId);

            //get all questions related to that certificate in a list
            var ctqList = (await _service.CertificateTopicQuestionService.GetAllCertificateTopicQuestionsAsync()).ToList();

            var ctList = (await _service.CertificateTopicService.GetAllCertificateTopicsAsync()).Where(ct => ct.Certificate == certificate).ToList();

            var myQuestionList = new List<CertificateExaminationQuestionDTO>();

            foreach (var ctq in ctqList)
            {
                foreach (var ct in ctList)
                {
                    if (ctq.CertificateTopic == ct)
                    {
                        await _service.CertificateTopicQuestionLoad(ctq);
                        myQuestionList.Add(new CertificateExaminationQuestionDTO
                        {
                            QuestionId = ctq.TopicQuestion.Question.QuestionId,
                            QuestionDisplay = ctq.TopicQuestion.Question.Display,
                        });
                    }
                }
            }

            var myModel = new ExaminationDTO
            {
                CertificateId = selectedCertificateId,
                Questions = myQuestionList
            };

            return View(myModel);



        }
        public async Task<IActionResult> SaveNewCertificateExamination(ExaminationDTO model, int[] selectedQuestions)
        {
            var examQuestions = new List<ExaminationQuestion>();

            var c = await _service.CertificateService.GetCertificateByIdAsync(model.CertificateId);
            var ctqList = (await _service.CertificateTopicQuestionService.GetAllCertificateTopicQuestionsAsync()).ToList();

            foreach (var item in ctqList)
            {
                await _service.CertificateTopicQuestionLoad(item);
            }

            foreach (var id in selectedQuestions)
            {
                var q = await _service.QuestionService.GetQuestionByIdAsync(id);

                var ctq = ctqList.Where(ctq => ctq.TopicQuestion.Question == q && ctq.CertificateTopic.Certificate == c);
                foreach (var item in ctq)
                {
                    examQuestions.Add(new ExaminationQuestion
                    {
                        CertificateTopicQuestion = item
                    });

                }
            }

            var newExamination = new Examination
            {
                Certificate = c,
                ExamQuestions = examQuestions
            };

            await _service.ExaminationService.AddExaminationAsync(newExamination);
            await _service.SaveChanges();

            return RedirectToAction("CertificateExaminationsIndex");
        }

        public async Task<IActionResult> UpdateCertificateExamination(int id)
        {
            var examination = await _service.ExaminationService.GetExaminationByIdAsync(id);
            await _service.LoadCertificates(examination);
            await _service.LoadExamQuestions(examination);

            //get all questions related to that certificate in a list
            var ctqList = (await _service.CertificateTopicQuestionService.GetAllCertificateTopicQuestionsAsync()).ToList();

            var ctList = (await _service.CertificateTopicService.GetAllCertificateTopicsAsync()).Where(ct => ct.Certificate == examination.Certificate).ToList();

            var myQuestionList = new List<CertificateExaminationQuestionDTO>();

            foreach (var ctq in ctqList)
            {
                foreach (var ct in ctList)
                {
                    if (ctq.CertificateTopic == ct)
                    {
                        await _service.CertificateTopicQuestionLoad(ctq);
                        myQuestionList.Add(new CertificateExaminationQuestionDTO
                        {
                            QuestionId = ctq.TopicQuestion.Question.QuestionId,
                            QuestionDisplay = ctq.TopicQuestion.Question.Display,
                        });
                    }
                }
            }

            var currentQuestion = examination.ExamQuestions;
            
            foreach (var ctq in currentQuestion)
            {
                await _service.LoadCTQ(ctq);
                foreach (var item in myQuestionList)
                {
                    if (ctq.CertificateTopicQuestion.TopicQuestion.Question.QuestionId == item.QuestionId)
                    {
                        item.IsSelected = true;
                    }

                }
            }

            var myModel = new ExaminationDTO
            {
                ExaminationId= examination.ExaminationId,
                CertificateId = examination.Certificate.CertificateId,
                Questions = myQuestionList
            };


            return View(myModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCertificateExamination(ExaminationDTO model, int[] selectedQuestions)
        {
            var examination = await _service.ExaminationService.GetExaminationByIdAsync(model.ExaminationId);
            await _service.LoadExamQuestions(examination);
            var examQuestions = new List<ExaminationQuestion>();

            var c = await _service.CertificateService.GetCertificateByIdAsync(model.CertificateId);
            var ctqList = (await _service.CertificateTopicQuestionService.GetAllCertificateTopicQuestionsAsync()).ToList();

            foreach (var item in ctqList)
            {
                await _service.CertificateTopicQuestionLoad(item);
            }

            foreach (var id in selectedQuestions)
            {
                var q = await _service.QuestionService.GetQuestionByIdAsync(id);

                var ctq = ctqList.Where(ctq => ctq.TopicQuestion.Question == q && ctq.CertificateTopic.Certificate == c);
                foreach (var item in ctq)
                {
                    examQuestions.Add(new ExaminationQuestion
                    {
                        CertificateTopicQuestion = item
                    });

                }
            }


            examination.ExamQuestions = examQuestions;

            await _service.ExaminationService.UpdateExaminationAsync(examination);
            await _service.SaveChanges();

            return RedirectToAction("CertificateExaminationsIndex");


        }

        public async Task<IActionResult> ConfirmDisableCertificateExamination(int? id)
        {
            if (id == null || await _service.ExaminationService.GetAllExaminationsAsync() == null)
            {
                return NotFound();
            }

            var certificateExamination = await _service.ExaminationService.GetExaminationByIdAsync(id);
            await _service.LoadCertificates(certificateExamination);

            if (certificateExamination == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<CertificateExaminationDTO>(certificateExamination));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisableCertificateExamination(int examinationId)
        {
            if (await _service.ExaminationService.GetAllExaminationsAsync() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Certificates'  is null.");
            }
            var certificateExamination = await _service.ExaminationService.GetExaminationByIdAsync(examinationId);

            if (certificateExamination != null)
            {
                certificateExamination.Status = "Unavailable";
                await _service.ExaminationService.UpdateExaminationAsync(certificateExamination);
            }

            await _service.SaveChanges();
            return RedirectToAction("CertificateExaminationsIndex");
        }




    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApp.MainServices.Interfaces;

namespace WebApp.Controllers
{
    public class GeneralHomeController : Controller
    {

        private readonly ICertificateManagerService _service;
        private readonly IMapper _mapper;

        public GeneralHomeController(ICertificateManagerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var certificates = await _service.CreateCertificateDTOs();
            return View(certificates);
        }
    }
}

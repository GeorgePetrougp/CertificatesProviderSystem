using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CertificatesController : Controller
    {
        public CertificatesController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

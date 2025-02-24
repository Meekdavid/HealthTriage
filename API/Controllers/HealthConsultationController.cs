using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/consult")]
    public class HealthConsultationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

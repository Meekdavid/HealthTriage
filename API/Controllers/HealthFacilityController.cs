using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/facility")]
    public class HealthFacilityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

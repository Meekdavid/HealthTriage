using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/articles")]
    public class HealthArticlesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

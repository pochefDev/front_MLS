using Microsoft.AspNetCore.Mvc;

namespace frontendLossSounds.Controllers
{
    public class MusicController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

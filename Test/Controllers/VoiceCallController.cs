using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    public class VoiceCallController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

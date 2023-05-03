using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    public class WhatsappMessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

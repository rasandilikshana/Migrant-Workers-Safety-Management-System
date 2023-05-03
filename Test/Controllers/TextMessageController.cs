using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    public class TextMessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

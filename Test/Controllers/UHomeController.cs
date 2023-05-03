using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    public class UHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

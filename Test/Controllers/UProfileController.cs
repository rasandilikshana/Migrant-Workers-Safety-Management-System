using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    public class UProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

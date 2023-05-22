using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    public class OfficeUserSignInController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    public class SignInController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

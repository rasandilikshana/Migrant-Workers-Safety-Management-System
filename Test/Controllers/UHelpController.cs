using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    public class UHelpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

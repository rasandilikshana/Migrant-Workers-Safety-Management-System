using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    public class PoliceOfficeUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

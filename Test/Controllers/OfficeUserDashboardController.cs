using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    public class OfficeUserDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

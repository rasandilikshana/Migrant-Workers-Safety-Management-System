using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    public class EmbassyOfficeUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    public class MailForwardingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

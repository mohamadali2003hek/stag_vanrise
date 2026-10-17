using Microsoft.AspNetCore.Mvc;

namespace task3.Controllers
{
    public class PhoneNumbersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

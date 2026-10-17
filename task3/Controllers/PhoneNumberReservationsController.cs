using Microsoft.AspNetCore.Mvc;

namespace task3.Controllers
{
    public class PhoneNumberReservationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

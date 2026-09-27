using Microsoft.AspNetCore.Mvc;

namespace task3.Controllers
{
    public class DevicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

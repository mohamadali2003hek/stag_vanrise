
using Microsoft.AspNetCore.Mvc;

namespace task3.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

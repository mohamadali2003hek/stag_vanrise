using Microsoft.AspNetCore.Mvc;

namespace task3.Controllers
{
    public class ClientsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

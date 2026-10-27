
using Microsoft.AspNetCore.Mvc;
using task3.Data;
using task3.Models;

namespace task3.Controllers
{
    public class ClientsReportController : Controller
    {
        private readonly ADOdotNET _ado;

        public ClientsReportController(ADOdotNET ado)
        {
            _ado = ado;
        }

        public IActionResult ClientsPerType(int? type)
        {
            ViewBag.SelectedType = type;

            var report = _ado.GetClientsPerTypeReport(type);

            return View(report);
        }
    }
}

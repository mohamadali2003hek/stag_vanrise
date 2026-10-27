
using Microsoft.AspNetCore.Mvc;
using task3.Data;

namespace task3.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ADOdotNET _ado;

        public ReportsController(ADOdotNET ado)
        {
            _ado = ado;
        }


        public IActionResult PhoneNumberStatus(
    int? deviceID,
    string? phoneNumberStatus)
        {
            ViewBag.Devices = _ado.GetAllDevices();
            ViewBag.SelectedDeviceID = deviceID;
            ViewBag.SelectedStatus = phoneNumberStatus;

            var report = _ado.GetPhoneNumberStatusReport(
                deviceID,
                string.IsNullOrWhiteSpace(phoneNumberStatus)
                    ? null
                    : phoneNumberStatus);

            return View("~/Views/ReportsController/Index.cshtml", report);
        }

    }
}


using Microsoft.AspNetCore.Mvc;
using task3.Data;

namespace task3.Controllers.Api
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardApiController : ControllerBase
    {
        private readonly ADOdotNET _ado;

        public DashboardApiController(ADOdotNET ado)
        {
            _ado = ado;
        }

        [HttpGet]
        public IActionResult GetDashboard()
        {
            var clientsByType = _ado.GetClientsPerTypeReport();
            var devices = _ado.GetAllDevices();
            var phoneNumbers = _ado.GetAllPhoneNumbers();
            var reservations = _ado.GetAllReservations();
            var phoneNumberStatus = _ado.GetPhoneNumberStatusReport();

            var now = DateTime.Now;

            var activeReservations = reservations.Count(r =>
                r.Bed <= now &&
                (r.Eed == null || r.Eed > now));

            var result = new
            {
                Summary = new
                {
                    TotalClients = clientsByType.Sum(c => c.NumberOfClients),
                    TotalDevices = devices.Count,
                    TotalPhoneNumbers = phoneNumbers.Count,
                    ActiveReservations = activeReservations
                },

                ClientsByType = clientsByType,

                ReservationStatus = phoneNumberStatus
                    .GroupBy(r => r.PhoneNumbersStatus)
                    .Select(g => new
                    {
                        Status = g.Key,
                        Count = g.Sum(r => r.NumberOfPhoneNumbers)
                    })
                    .ToList(),

                PhoneNumbersByDevice = phoneNumbers
                    .GroupBy(p => new { p.DeviceId, p.DeviceName })
                    .Select(g => new
                    {
                        DeviceId = g.Key.DeviceId,
                        DeviceName = g.Key.DeviceName,
                        Count = g.Count()
                    })
                    .ToList(),

                RecentReservations = reservations
                    .OrderByDescending(r => r.Bed)
                    .Take(5)
                    .ToList()
            };

            return Ok(result);
        }
    }
}

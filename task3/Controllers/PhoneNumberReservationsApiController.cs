using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task3.Data;
using task3.Models;

namespace task3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneNumberReservationsApiController : ControllerBase
    {
        private readonly ADOdotNET _ADOdotNET;
        public PhoneNumberReservationsApiController(ADOdotNET ADOdotNET) { _ADOdotNET = ADOdotNET; }

        [HttpGet]
        public List<PhoneNumberReservation> GetAll() => _ADOdotNET.GetFilteredReservations(null, null);

        [HttpGet("filter")]
        public List<PhoneNumberReservation> Filter([FromQuery] int? clientId, [FromQuery] int? phoneNumberId)
            => _ADOdotNET.GetFilteredReservations(clientId, phoneNumberId);
    }

}

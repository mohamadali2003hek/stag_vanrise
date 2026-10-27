
using Microsoft.AspNetCore.Mvc;
using task3.Data;

namespace task3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsControllerApi : ControllerBase
    {
        private readonly ADOdotNET _ado;

        public ReportsControllerApi(ADOdotNET ado)
        {
            _ado = ado;
        }

        [HttpGet("PhoneNumberStatus")]
        public IActionResult GetPhoneNumberStatusReport(
            [FromQuery] int? deviceID = null,
            [FromQuery] string? phoneNumberStatus = null)
        {
            if (phoneNumberStatus != null &&
                phoneNumberStatus != "Reserved" &&
                phoneNumberStatus != "Unreserved")
            {
                return BadRequest(
                    "phoneNumberStatus must be Reserved or Unreserved.");
            }

            var result = _ado.GetPhoneNumberStatusReport(
                deviceID,
                phoneNumberStatus);

            return Ok(result);
        }
    }
}

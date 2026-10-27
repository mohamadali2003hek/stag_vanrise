
using Microsoft.AspNetCore.Mvc;
using task3.Data;

namespace task3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsReportControllerApi : ControllerBase
    {
        private readonly ADOdotNET _ado;

        public ClientsReportControllerApi(ADOdotNET ado)
        {
            _ado = ado;
        }

        [HttpGet("ClientsPerType")]
        public IActionResult GetClientsPerTypeReport(
            [FromQuery] int? type = null)
        {
            if (type.HasValue && type.Value != 0 && type.Value != 1)
            {
                return BadRequest(
                    "Type must be 0 (Individual) or 1 (Organization).");
            }

            var result = _ado.GetClientsPerTypeReport(type);

            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task3.Data;
using task3.Models;

namespace task3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsApiController : ControllerBase
    {
        private readonly ADOdotNET _ADOdotNET;

        public ClientsApiController(ADOdotNET ADOdotNET)
        {
            _ADOdotNET = ADOdotNET;
        }
        [HttpGet]
        public List<Client> GetAllClients() => _ADOdotNET.GetAllClients();

        [HttpGet("filter")]
        public List<Client> GetFilteredClients([FromQuery] string? name, [FromQuery] ClientType? type)
        {
            return _ADOdotNET.GetFilteredClients(name, type);
        }

        [HttpPost]
        public Client AddClient(Client client)
        {
            return _ADOdotNET.AddClient(client);
        }

        [HttpPost("update")]
        public Client UpdateClient(Client client)
        {
            return _ADOdotNET.UpdateClient(client);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteClient(int id)
        {
            if (!_ADOdotNET.DeleteClient(id))
                return Conflict("Cannot delete this client: it has phone number reservations.");
            return Ok();
        }

        public class ReserveRequest { public int PhoneNumberId { get; set; } }

        [HttpGet("{id:int}/reservations")]
        public List<PhoneNumberReservation> GetActiveReservations(int id)
            => _ADOdotNET.GetActiveReservationsByClient(id);

        [HttpPost("{id:int}/reserve")]
        public IActionResult Reserve(int id, [FromBody] ReserveRequest request)
        {
            if (request.PhoneNumberId <= 0) return BadRequest("Phone number is required.");

            var reservationId = _ADOdotNET.ReservePhoneNumber(id, request.PhoneNumberId);
            if (reservationId == 0)
                return Conflict("This phone number is no longer available.");

            return Ok(reservationId);
        }

        [HttpPost("end-reservation/{reservationId:int}")]
        public IActionResult EndReservation(int reservationId)
        {
            if (!_ADOdotNET.EndReservation(reservationId))
                return Conflict("This reservation is not active.");

            return Ok();
        }
    }
}

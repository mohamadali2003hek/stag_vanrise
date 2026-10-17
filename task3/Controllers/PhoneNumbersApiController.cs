using Microsoft.AspNetCore.Mvc;
using task3.Data;
using task3.dto;
using task3.Models;

namespace task3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneNumbersApiController : ControllerBase
    {
        private readonly ADOdotNET _ADOdotNET;

        public PhoneNumbersApiController(ADOdotNET ADOdotNET)
        {
            _ADOdotNET = ADOdotNET;
        }

        [HttpGet]
        public List<PhoneNumberdto> GetAllPhoneNumbers()
        {
            return _ADOdotNET.GetAllPhoneNumbers();
        }

        [HttpGet("filter")]
        public List<PhoneNumber> GetFilteredPhoneNumbers(
            string? number,
            int? deviceId)
        {
            return _ADOdotNET.GetFilteredPhoneNumbers(number, deviceId);
        }

        [HttpPost]
        public PhoneNumber AddPhoneNumber(PhoneNumber phoneNumber)
        {
            return _ADOdotNET.AddPhoneNumber(phoneNumber);
        }

        [HttpPost("update")]
        public PhoneNumber UpdatePhoneNumber(PhoneNumber phoneNumber)
        {
            return _ADOdotNET.UpdatePhoneNumber(phoneNumber);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!_ADOdotNET.DeletePhoneNumber(id))
                return Conflict("Cannot delete this phone number: it has reservations.");
            return Ok();
        }

        [HttpGet("free")]
        public List<PhoneNumber> GetFree() => _ADOdotNET.GetFreePhoneNumbers();
    }
}
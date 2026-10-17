using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using task3.Data;
using task3.Models;

namespace task3.Controllers
{
    [Route("api/devices")]
    [ApiController]
    public class DevicesApiController : ControllerBase
    {

        private readonly ADOdotNET _ADOdotNET;

        public DevicesApiController(ADOdotNET ADOdotNET)
        {
            _ADOdotNET = ADOdotNET;
        }
        [HttpGet]
        public List<Device> GetAllDevices() => _ADOdotNET.GetAllDevices();

        [HttpGet("filter")]
        public List<Device> GetFilteredDevices(string name)
        {
            return _ADOdotNET.GetFilteredDevices(name);
        }

        [HttpPost]
        public Device AddDevice(Device device)
        {
            return _ADOdotNET.AddDevice(device);
        }

        [HttpPost("update")]
        public Device UpdateDevice(Device device)
        {
            return _ADOdotNET.UpdateDevice(device);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!_ADOdotNET.DeleteDevice(id))
                return Conflict("Cannot delete this device: it has phone numbers assigned to it.");

            return Ok();
        }
    }
}

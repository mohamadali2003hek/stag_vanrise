using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using task3.Models;

namespace task3.Controllers
{
    [Route("api/devices")]
    [ApiController]
    public class DevicesApiController : ControllerBase
    {


        [HttpGet]
        public List<Device> GetAllDevices() => Data.DeviceData.devices;

        [HttpGet("filter")]
        public List<Device> GetFilteredDevices(string name)
        {
            return Data.DeviceData.devices.Where(d => d.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        [HttpPost]
        public Device AddDevice(Device device)
        {
            device.Id = Data.DeviceData.devices.Max(d => d.Id) + 1;
            Data.DeviceData.devices.Add(device);
            return device;
        }

        [HttpPost("update")]
        public Device UpdateDevice(Device device)
        {
            var existingDevice =
                Data.DeviceData.devices.FirstOrDefault(d => d.Id == device.Id);

            if (existingDevice != null)
            {
                existingDevice.Name = device.Name;
                return existingDevice;
            }

            return null;
        }
    }
}

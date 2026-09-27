using task3.Models;

namespace task3.Data
{
    public class DeviceData
    {

        public static List<Device> devices = new List<Device>
        {
            new Device { Id = 1, Name = "Device 1" },
            new Device { Id = 2, Name = "Device 2" },
            new Device { Id = 3, Name = "Device 3" }
        };
    }
}

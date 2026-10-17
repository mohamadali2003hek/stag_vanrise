using task3.dto;
using task3.Models;

namespace task3.Data.Mapper
{
    public class PhoneNumberMapperdto
    {
        public Func<int, string, int, string, PhoneNumberdto> Mapper { get; }

        public PhoneNumberMapperdto()
        {
            Mapper = Map;
        }

        public PhoneNumberdto Map(int id, string number, int deviceId ,string deviceName)
        {
            return new PhoneNumberdto
            {
                Id = id,
                Number = number,
                DeviceId = deviceId,
                DeviceName = deviceName
            };
        }
    }
}

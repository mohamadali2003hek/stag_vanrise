using task3.dto;
using task3.Models;

namespace task3.Data.Mapper
{
    public class PhoneNumberMapper
    {
        public Func<int, string, int, PhoneNumber> Mapper { get; }

        public PhoneNumberMapper()
        {
            Mapper = Map;
        }

        public PhoneNumber Map(int id, string number, int deviceId )
        {
            return new PhoneNumber
            {
                Id = id,
                Number = number,
                DeviceId = deviceId
                
            };
        }
    }
}

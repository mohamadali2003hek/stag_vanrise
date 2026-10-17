using task3.Models;

namespace task3.Data.Mapper
{
    public class DeviceMapper
    {
        public Func<int, string, Device> Mapper { get; }
        
        public DeviceMapper(){

            Mapper = Map;
        }
        public Device Map(int id, string name)
        {
            return new Device
            {
                Id = id,
                Name = name
            };
        }
    }
}
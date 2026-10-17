using task3.Models;

namespace task3.Data.Mapper
{
    public class ClientMapper
    {

        Func<int, string, ClientType, DateTime?, Client> Mapper { get; }

        public ClientMapper()
        {
            Mapper = Map;
        }


        public Client Map(int id, string name, ClientType type, DateTime? birthDate)
        {
            return new Client
            {
                Id = id,
                Name = name,
                Type = type,
                BirthDate = birthDate
            };
        }
    }
}

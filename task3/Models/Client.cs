namespace task3.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ClientType Type { get; set; }

        public DateTime? BirthDate { get; set; }
    }

    public enum ClientType
    {
        Individual,
        Organization
    }
}
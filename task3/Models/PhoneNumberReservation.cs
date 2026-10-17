namespace task3.Models
{
    public class PhoneNumberReservation
    {

        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = "";
        public int PhoneNumberId { get; set; }
        public string PhoneNumber { get; set; } = "";
        public DateTime Bed { get; set; }
        public DateTime? Eed { get; set; }   // null = no end date
    }
}


using task3.dto;

namespace task3.Models
{
    public class DashboardViewModel
    {
        public int TotalClients { get; set; }
        public int TotalDevices { get; set; }
        public int TotalPhoneNumbers { get; set; }
        public int ActiveReservations { get; set; }

        public List<ClientsPerTypeReport> ClientsByType { get; set; }
            = new();

        public List<PhoneNumberStatusReport> PhoneNumberStatus { get; set; }
            = new();

        public List<PhoneNumberdto> PhoneNumbersByDevice { get; set; }
            = new();

        public List<PhoneNumberReservation> RecentReservations { get; set; }
            = new();
    }
}

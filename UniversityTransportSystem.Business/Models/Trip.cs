namespace UniversityTransportSystem.Business.Models
{
    public class Trip
    {
        public int TripID { get; set; }
        public int BusID { get; set; }
        public int DriverID { get; set; }
        public int TransportLineID { get; set; }
        public int ScheduleID { get; set; }
        public DateTime TripDate { get; set; }
        public TimeSpan? ActualDepartureTime { get; set; }
        public TimeSpan? ActualArrivalTime { get; set; }
        public bool TripStatus { get; set; }
        public int DelayInMinutes { get; set; }
    }
}

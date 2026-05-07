namespace UniversityTransportSystem.Business.Models
{
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public int TransportLineID { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public bool ScheduleStatus { get; set; }
    }
}

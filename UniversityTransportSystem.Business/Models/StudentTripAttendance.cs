namespace UniversityTransportSystem.Business.Models
{
    public class StudentTripAttendance
    {
        public int StudentTripAttendanceID { get; set; }
        public int StudentID { get; set; }
        public int TripID { get; set; }
        public int? BoardingStationID { get; set; }
        public int? DropOffStationID { get; set; }
        public TimeSpan? BoardingTime { get; set; }
        public TimeSpan? DropOffTime { get; set; }
        public bool AttendanceStatus { get; set; }
        public string? Notes { get; set; }
    }
}

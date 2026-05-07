namespace UniversityTransportSystem.Business.Models
{
    public class BusAssignment
    {
        public int BusAssignmentID { get; set; }
        public int BusID { get; set; }
        public int TransportLineID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool AssignmentStatus { get; set; }
    }
}

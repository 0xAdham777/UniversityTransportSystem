namespace UniversityTransportSystem.Business.Models
{
    public class Incident
    {
        public int IncidentID { get; set; }
        public int TripID { get; set; }
        public int ReportedByEmployeeID { get; set; }
        public int IncidentTypeID { get; set; }
        public string? IncidentDescription { get; set; }
        public DateTime IncidentDateTime { get; set; }
    }
}

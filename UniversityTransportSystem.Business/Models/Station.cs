namespace UniversityTransportSystem.Business.Models
{
    public class Station
    {
        public int StationID { get; set; }
        public string StationName { get; set; }
        public string? LocationDescription { get; set; }
        public int MunicipalityID { get; set; }
        public bool StationStatus { get; set; }
    }
}

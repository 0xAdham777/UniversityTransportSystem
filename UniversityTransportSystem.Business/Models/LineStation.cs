namespace UniversityTransportSystem.Business.Models
{
    public class LineStation
    {
        public int LineStationID { get; set; }
        public int TransportLineID { get; set; }
        public int StationID { get; set; }
        public int StationOrder { get; set; }
        public decimal? DistanceFromOrigin { get; set; }
    }
}

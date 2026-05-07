namespace UniversityTransportSystem.Business.Models
{
    public class TransportLine
    {
        public int TransportLineID { get; set; }
        public string LineName { get; set; }
        public int OriginStationID { get; set; }
        public int DestinationStationID { get; set; }
        public bool LineStatus { get; set; }
    }
}

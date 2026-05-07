namespace UniversityTransportSystem.Business.Models
{
    public class Bus
    {
        public int BusID { get; set; }
        public int BusModelID { get; set; }
        public string PlateNumber { get; set; }
        public string? BusCode { get; set; }
        public int? ManufacturingYear { get; set; }
        public bool BusStatus { get; set; }
    }
}

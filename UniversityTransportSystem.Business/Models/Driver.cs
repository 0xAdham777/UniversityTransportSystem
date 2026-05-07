namespace UniversityTransportSystem.Business.Models
{
    public class Driver
    {
        public int DriverID { get; set; }
        public int EmployeeID { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime LicenseExpiryDate { get; set; }
        public bool DriverStatus { get; set; }
    }
}

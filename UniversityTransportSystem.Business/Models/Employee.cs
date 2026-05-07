namespace UniversityTransportSystem.Business.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public int PersonID { get; set; }
        public DateTime HireDate { get; set; }
        public bool EmployeeStatus { get; set; }
    }
}

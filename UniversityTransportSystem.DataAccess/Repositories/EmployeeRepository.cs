using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.Business.Models;

namespace UniversityTransportSystem.DataAccess.Repositories;

public class EmployeeRepository : BaseRepository<Employee>
{
    public EmployeeRepository() : base("Employee")
    {
    }
}

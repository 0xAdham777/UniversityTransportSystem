using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.Business.Models;

namespace UniversityTransportSystem.DataAccess.Repositories;

public class DepartmentRepository : BaseRepository<Department>
{
    public DepartmentRepository() : base("Department")
    {
    }
}

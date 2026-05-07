using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.Business.Models;

namespace UniversityTransportSystem.DataAccess.Repositories;

public class BusAssignmentRepository : BaseRepository<BusAssignment>
{
    public BusAssignmentRepository() : base("BusAssignment")
    {
    }
}

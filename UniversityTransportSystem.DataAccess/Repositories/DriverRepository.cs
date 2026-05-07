using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.Business.Models;

namespace UniversityTransportSystem.DataAccess.Repositories;

public class DriverRepository : BaseRepository<Driver>
{
    public DriverRepository() : base("Driver")
    {
    }
}

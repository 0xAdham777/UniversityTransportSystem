using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.Business.Models;

namespace UniversityTransportSystem.DataAccess.Repositories;

public class BusModelRepository : BaseRepository<BusModel>
{
    public BusModelRepository() : base("BusModel")
    {
    }
}

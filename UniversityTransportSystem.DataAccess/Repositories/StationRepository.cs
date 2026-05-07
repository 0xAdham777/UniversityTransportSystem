using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.Business.Models;

namespace UniversityTransportSystem.DataAccess.Repositories;

public class StationRepository : BaseRepository<Station>
{
    public StationRepository() : base("Station")
    {
    }
}

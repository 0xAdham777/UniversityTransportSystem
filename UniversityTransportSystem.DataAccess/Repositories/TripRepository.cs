using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.Business.Models;

namespace UniversityTransportSystem.DataAccess.Repositories;

public class TripRepository : BaseRepository<Trip>
{
    public TripRepository() : base("Trip")
    {
    }
}

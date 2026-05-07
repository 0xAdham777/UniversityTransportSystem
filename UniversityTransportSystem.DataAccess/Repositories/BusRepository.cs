using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.Business.Models;

namespace UniversityTransportSystem.DataAccess.Repositories;

public class BusRepository : BaseRepository<Bus>
{
    public BusRepository() : base("Bus")
    {
    }
}

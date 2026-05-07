using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.Business.Models;

namespace UniversityTransportSystem.DataAccess.Repositories;

public class WilayaRepository : BaseRepository<Wilaya>
{
    public WilayaRepository() : base("Wilaya")
    {
    }
}

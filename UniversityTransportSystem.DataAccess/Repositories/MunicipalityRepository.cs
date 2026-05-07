using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.Business.Models;

namespace UniversityTransportSystem.DataAccess.Repositories;

public class MunicipalityRepository : BaseRepository<Municipality>
{
    public MunicipalityRepository() : base("Municipality")
    {
    }
}

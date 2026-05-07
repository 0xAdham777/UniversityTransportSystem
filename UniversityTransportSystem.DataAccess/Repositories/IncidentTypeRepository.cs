using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.Business.Models;

namespace UniversityTransportSystem.DataAccess.Repositories;

public class IncidentTypeRepository : BaseRepository<IncidentType>
{
    public IncidentTypeRepository() : base("IncidentType")
    {
    }
}

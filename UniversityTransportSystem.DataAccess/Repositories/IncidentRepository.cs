using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.Business.Models;

namespace UniversityTransportSystem.DataAccess.Repositories;

public class IncidentRepository : BaseRepository<Incident>
{
    public IncidentRepository() : base("Incident")
    {
    }
}

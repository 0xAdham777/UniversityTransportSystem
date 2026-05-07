using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.Business.Models;

namespace UniversityTransportSystem.DataAccess.Repositories;

public class LineStationRepository : BaseRepository<LineStation>
{
    public LineStationRepository() : base("LineStation")
    {
    }
}

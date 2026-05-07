using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.Business.Models;

namespace UniversityTransportSystem.DataAccess.Repositories;

public class TransportLineRepository : BaseRepository<TransportLine>
{
    public TransportLineRepository() : base("TransportLine")
    {
    }
}

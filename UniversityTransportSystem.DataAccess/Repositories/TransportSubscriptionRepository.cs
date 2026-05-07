using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.Business.Models;

namespace UniversityTransportSystem.DataAccess.Repositories;

public class TransportSubscriptionRepository : BaseRepository<TransportSubscription>
{
    public TransportSubscriptionRepository() : base("TransportSubscription")
    {
    }
}

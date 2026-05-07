using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.Business.Models;

namespace UniversityTransportSystem.DataAccess.Repositories;

public class SubscriptionPaymentRepository : BaseRepository<SubscriptionPayment>
{
    public SubscriptionPaymentRepository() : base("SubscriptionPayment")
    {
    }
}

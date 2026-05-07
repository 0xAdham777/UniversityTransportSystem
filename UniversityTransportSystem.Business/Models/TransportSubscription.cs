namespace UniversityTransportSystem.Business.Models
{
    public class TransportSubscription
    {
        public int TransportSubscriptionID { get; set; }
        public int StudentID { get; set; }
        public int TransportLineID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool SubscriptionStatus { get; set; }
    }
}

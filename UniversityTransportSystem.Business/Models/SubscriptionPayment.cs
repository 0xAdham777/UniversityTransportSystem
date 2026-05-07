namespace UniversityTransportSystem.Business.Models
{
    public class SubscriptionPayment
    {
        public int PaymentID { get; set; }
        public int TransportSubscriptionID { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool? PaymentStatus { get; set; }
    }
}

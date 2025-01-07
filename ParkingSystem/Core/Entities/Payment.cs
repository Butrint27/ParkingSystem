namespace ParkingSystem.Core.Entities
{
    public class Payment
    {
        public int id { get; set; }
        public float amount { get; set; }
        public DateTime date { get; set; }
        public String status { get; set; }

        //lidhja
        public int PaymentId { get; set; }
        public PaymentMethod paymentMethod { get; set; }
    }
}

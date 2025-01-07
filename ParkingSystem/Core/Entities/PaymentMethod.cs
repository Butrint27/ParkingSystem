using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.Core.Entities
{
    public class PaymentMethod
    {
        [Key]
        public int id { get; set; }
        public String type { get; set; }
        public String details { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}

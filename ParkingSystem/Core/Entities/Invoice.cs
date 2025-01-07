using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.Core.Entities
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime DateGenerated { get; set; }
        public decimal TotalAmount { get; set; }

        // Çelësi i huaj dhe lidhja me Payment
        [ForeignKey("Payment")]
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}

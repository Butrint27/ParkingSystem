namespace ParkingSystem.Core.Dtos.Invoice
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime DateGenerated { get; set; }
        public decimal TotalAmount { get; set; }
        public int PaymentId { get; set; }
    }
}

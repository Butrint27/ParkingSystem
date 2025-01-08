using ParkingSystem.Core.Dtos.PaymentMethod;

public class PaymentDto
{
    public int Id { get; set; }
    public float Amount { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; }
    public int PaymentId { get; set; }
    public PaymentMethodDto PaymentMethod { get; set; }
}
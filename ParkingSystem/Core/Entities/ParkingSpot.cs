using System.ComponentModel.DataAnnotations;

public class ParkingSpot
{
    [Key]
    public int Id { get; set; } // ID është tani int
    public string Location { get; set; }
    public string Size { get; set; }
    public string Status { get; set; }
    public decimal PricePerHour { get; set; }

    // Navigation property
    public List<Reservation> Reservations { get; set; }
}

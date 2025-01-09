using ParkingSystem.Core.Entities;
using System.ComponentModel.DataAnnotations;

public class Reservation
{
    [Key]
    public int Id { get; set; } // ID �sht� tani int
    public int UserId { get; set; }
    public int ParkingSpotId { get; set; }  // Foreign key n� ParkingSpot �sht� int tani
    public int ParkingReservationManagerId { get; set; } // Foreign key n� ParkingReservationManager �sht� int tani
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public ParkingSpot ParkingSpot { get; set; }
    public List<ParkingReservationManager> ParkingReservationManagers { get; set; }
}

using ParkingSystem.Core.Entities;
using System.ComponentModel.DataAnnotations;

public class ParkingReservationManager
{
    [Key]
    public int Id { get; set; } // ID është tani int
    public string ParkingSpotSize { get; set; }

    public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    public List<ParkingSpot> ParkingSpots { get; set; } = new List<ParkingSpot>();

    public string ManagerName { get; set; }
    public string ManagerContact { get; set; }
}

using ParkingSystem;

public class AvailabilityMonitor
{
    public List<ParkingSpace> ParkingSpaces { get; set; } = new List<ParkingSpace>();
    public List<ParkingSpace> AvailableSlots { get; set; } = new List<ParkingSpace>();
}

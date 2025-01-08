using ParkingSystem;

public class ParkingSpaceManager
{
    public List<ParkingSpace> ParkingSpaces { get; set; } = new List<ParkingSpace>();
    public AvailabilityMonitor AvailableMonitor { get; set; }
    
}
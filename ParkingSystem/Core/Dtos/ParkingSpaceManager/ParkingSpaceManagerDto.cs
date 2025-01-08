using ParkingSystem.Core.Dtos;

public class ParkingSpaceManagerDto
{
    public List<ParkingSpaceDto> ParkingSpaces { get; set; } = new List<ParkingSpaceDto>();
    public AvailabilityMonitorDto AvailableMonitor { get; set; }
}

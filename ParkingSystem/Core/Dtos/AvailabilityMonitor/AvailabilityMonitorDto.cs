using ParkingSystem.Core.Dtos;

public class AvailabilityMonitorDto
{
    public List<ParkingSpaceDto> ParkingSpaces { get; set; } = new List<ParkingSpaceDto>();
    public List<ParkingSpaceDto> AvailableSlots { get; set; } = new List<ParkingSpaceDto>();
}
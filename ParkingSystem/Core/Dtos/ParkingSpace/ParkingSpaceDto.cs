namespace ParkingSystem.Core.Dtos
{
    public class ParkingSpaceDto
    {
     
        public string Location { get; set; }
        public string Size { get; set; }
        public string Status { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public decimal PricePerHour { get; set; }
    }
}

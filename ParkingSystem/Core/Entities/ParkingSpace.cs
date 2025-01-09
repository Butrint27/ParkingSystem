using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingSystem
{
    public class ParkingSpace
    {
        [Key]
        public int Id { get; set; }
        public string Location { get; set; }
        public string Size { get; set; }
        public string Status { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public decimal PricePerHour { get; set; }
    }
}
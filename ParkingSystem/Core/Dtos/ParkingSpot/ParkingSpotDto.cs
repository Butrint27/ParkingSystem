using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingSystem.Core.Dtos.ParkingSpot
{
    public class ParkingSpotDto
    {
     
        public string Location { get; set; }
        public string Size { get; set; }
        public string Status { get; set; }
        public decimal PricePerHour { get; set; }
    }
}
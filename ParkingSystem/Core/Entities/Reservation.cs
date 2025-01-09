using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingSystem.Core.Entities
{
     public class Reservation
    {
        [Key]
        public string Id { get; set; }
        public int UserId { get; set; }
        public string ParkingSpotId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

      
        public ParkingSpot ParkingSpot { get; set; }
        public List<ParkingReservationManager> ParkingReservationManagers { get; set; }
    }
}
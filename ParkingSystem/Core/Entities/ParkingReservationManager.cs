using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingSystem.Core.Entities
{
    public class ParkingReservationManager
    {
        [Key]
        public string Id { get; set; } 
        public string ParkingSpotSize { get; set; } 

        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

        public List<ParkingSpot> ParkingSpots { get; set; } = new List<ParkingSpot>();

        public string ManagerName { get; set; } 
        public string ManagerContact { get; set; } 
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingSystem.Core.Entities
{
    public class ParkingReservationManager
    {
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
        public List<ParkingSpot> ParkingSpots { get; set; } = new List<ParkingSpot>();
    }
}
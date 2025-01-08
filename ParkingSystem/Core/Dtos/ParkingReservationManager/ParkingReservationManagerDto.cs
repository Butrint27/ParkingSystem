using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingSystem.Core.Dtos.ParkingSpot;
using ParkingSystem.Core.Dtos.Reservation;

namespace ParkingSystem.Core.Dtos.ParkingReservationManager
{
    public class ParkingReservationManagerDto
    {
        public List<ReservationDto> Reservations { get; set; } = new List<ReservationDto>();
        public List<ParkingSpotDto> ParkingSpots { get; set; } = new List<ParkingSpotDto>();
    }
}
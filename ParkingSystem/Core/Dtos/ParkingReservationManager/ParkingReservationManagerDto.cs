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
        public string ManagerName { get; set; }
        public string ManagerContact { get; set; }
    }
}
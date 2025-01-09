using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingSystem.Core.Dtos.Reservation
{
    public class ReservationDto
    {
  
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
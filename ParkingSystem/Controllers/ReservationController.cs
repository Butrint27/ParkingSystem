using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingSystem.Core.DbContext;
using ParkingSystem.Core.Dtos.Reservation;
using ParkingSystem.Core.Entities;

namespace ParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ReservationController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationDto dto)
        {
            var newReservation = _mapper.Map<Reservation>(dto);
            newReservation.CreatedAt = DateTime.UtcNow;
            newReservation.UpdatedAt = DateTime.UtcNow;

            await _context.Reservations.AddAsync(newReservation);
            await _context.SaveChangesAsync();

            return Ok("Reservation Created Successfully");
        }

        // Read All
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAllReservations()
        {
            var reservations = await _context.Reservations
                .Include(r => r.ParkingSpot)
                .Include(r => r.ParkingReservationManagers)
                .ToListAsync();

            var convertedReservations = _mapper.Map<IEnumerable<ReservationDto>>(reservations);
            return Ok(convertedReservations);
        }

        // Read by ID
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ReservationDto>> GetReservationById([FromRoute] string id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.ParkingSpot)
                .Include(r => r.ParkingReservationManagers)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation is null)
            {
                return NotFound("Reservation Not Found");
            }

            var convertedReservation = _mapper.Map<ReservationDto>(reservation);
            return Ok(convertedReservation);
        }

        // Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateReservation([FromRoute] string id, [FromBody] ReservationDto dto)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id);
            if (reservation is null)
            {
                return NotFound("Reservation Not Found");
            }

            reservation.StartDate = dto.StartDate;
            reservation.EndDate = dto.EndDate;
            reservation.Status = dto.Status;
            reservation.TotalAmount = dto.TotalAmount;
            reservation.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok("Reservation Updated Successfully");
        }

        // Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteReservation([FromRoute] string id)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id);
            if (reservation is null)
            {
                return NotFound("Reservation Not Found");
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return Ok("Reservation Deleted");
        }
    }
}

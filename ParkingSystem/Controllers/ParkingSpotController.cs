using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingSystem.Core.DbContext;
using ParkingSystem.Core.Dtos.ParkingSpot;
using ParkingSystem.Core.Entities;

namespace ParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingSpotController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ParkingSpotController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateParkingSpot([FromBody] ParkingSpotDto dto)
        {
            var newParkingSpot = _mapper.Map<ParkingSpot>(dto);

            await _context.ParkingSpots.AddAsync(newParkingSpot);
            await _context.SaveChangesAsync();

            return Ok("Parking Spot Created Successfully");
        }

        // Read All
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<ParkingSpotDto>>> GetAllParkingSpots()
        {
            var parkingSpots = await _context.ParkingSpots
                .Include(ps => ps.Reservations)
                .ToListAsync();

            var convertedSpots = _mapper.Map<IEnumerable<ParkingSpotDto>>(parkingSpots);
            return Ok(convertedSpots);
        }

        // Read by ID
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ParkingSpotDto>> GetParkingSpotById([FromRoute] string id)
        {
            var parkingSpot = await _context.ParkingSpots
                .Include(ps => ps.Reservations)
                .FirstOrDefaultAsync(ps => ps.Id == id);

            if (parkingSpot is null)
            {
                return NotFound("Parking Spot Not Found");
            }

            var convertedSpot = _mapper.Map<ParkingSpotDto>(parkingSpot);
            return Ok(convertedSpot);
        }

        // Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateParkingSpot([FromRoute] string id, [FromBody] ParkingSpotDto dto)
        {
            var parkingSpot = await _context.ParkingSpots.FirstOrDefaultAsync(ps => ps.Id == id);
            if (parkingSpot is null)
            {
                return NotFound("Parking Spot Not Found");
            }

            parkingSpot.Location = dto.Location;
            parkingSpot.Size = dto.Size;
            parkingSpot.Status = dto.Status;
            parkingSpot.PricePerHour = dto.PricePerHour;

            await _context.SaveChangesAsync();
            return Ok("Parking Spot Updated Successfully");
        }

        // Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteParkingSpot([FromRoute] string id)
        {
            var parkingSpot = await _context.ParkingSpots.FirstOrDefaultAsync(ps => ps.Id == id);
            if (parkingSpot is null)
            {
                return NotFound("Parking Spot Not Found");
            }

            _context.ParkingSpots.Remove(parkingSpot);
            await _context.SaveChangesAsync();
            return Ok("Parking Spot Deleted");
        }
    }
}

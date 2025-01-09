using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingSystem.Core.DbContext;
using ParkingSystem.Core.Dtos;
using ParkingSystem.Core.Entities;

namespace ParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingSpaceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ParkingSpaceController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateParkingSpace([FromBody] ParkingSpaceDto dto)
        {
            var newParkingSpace = _mapper.Map<ParkingSpace>(dto);

            newParkingSpace.CreatedAt = DateTime.UtcNow.ToString("o");
            newParkingSpace.UpdatedAt = DateTime.UtcNow.ToString("o");

            await _context.ParkingSpaces.AddAsync(newParkingSpace);
            await _context.SaveChangesAsync();

            return Ok("Parking Space Created Successfully");
        }

        // Read All
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<ParkingSpaceDto>>> GetAllParkingSpaces()
        {
            var parkingSpaces = await _context.ParkingSpaces.ToListAsync();
            var convertedSpaces = _mapper.Map<IEnumerable<ParkingSpaceDto>>(parkingSpaces);

            return Ok(convertedSpaces);
        }

        // Read by ID
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ParkingSpaceDto>> GetParkingSpaceById([FromRoute] int id)
        {
            var parkingSpace = await _context.ParkingSpaces.FirstOrDefaultAsync(ps => ps.Id == id);

            if (parkingSpace is null)
            {
                return NotFound("Parking Space Not Found");
            }

            var convertedSpace = _mapper.Map<ParkingSpaceDto>(parkingSpace);
            return Ok(convertedSpace);
        }

        // Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateParkingSpace([FromRoute] int id, [FromBody] ParkingSpaceDto dto)
        {
            var parkingSpace = await _context.ParkingSpaces.FirstOrDefaultAsync(ps => ps.Id == id);
            if (parkingSpace is null)
            {
                return NotFound("Parking Space Not Found");
            }

            parkingSpace.Location = dto.Location;
            parkingSpace.Size = dto.Size;
            parkingSpace.Status = dto.Status;
            parkingSpace.PricePerHour = dto.PricePerHour;
            parkingSpace.UpdatedAt = DateTime.UtcNow.ToString("o");

            await _context.SaveChangesAsync();
            return Ok("Parking Space Updated Successfully");
        }

        // Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteParkingSpace([FromRoute] int id)
        {
            var parkingSpace = await _context.ParkingSpaces.FirstOrDefaultAsync(ps => ps.Id == id);
            if (parkingSpace is null)
            {
                return NotFound("Parking Space Not Found");
            }

            _context.ParkingSpaces.Remove(parkingSpace);
            await _context.SaveChangesAsync();
            return Ok("Parking Space Deleted");
        }
    }
}

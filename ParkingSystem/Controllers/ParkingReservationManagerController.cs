using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingSystem.Core.DbContext;
using ParkingSystem.Core.Dtos.ParkingReservationManager;
using ParkingSystem.Core.Entities;

namespace ParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingReservationManagerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ParkingReservationManagerController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateManager([FromBody] ParkingReservationManagerDto dto)
        {
            var newManager = _mapper.Map<ParkingReservationManager>(dto);

            await _context.ParkingReservationManagers.AddAsync(newManager);
            await _context.SaveChangesAsync();

            return Ok("Parking Reservation Manager Created Successfully");
        }

        // Read All
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<ParkingReservationManagerDto>>> GetAllManagers()
        {
            var managers = await _context.ParkingReservationManagers
                .Include(m => m.Reservations)
                .Include(m => m.ParkingSpots)
                .ToListAsync();

            var convertedManagers = _mapper.Map<IEnumerable<ParkingReservationManagerDto>>(managers);
            return Ok(convertedManagers);
        }

        // Read by ID
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ParkingReservationManagerDto>> GetManagerById([FromRoute] string id)
        {
            var manager = await _context.ParkingReservationManagers
                .Include(m => m.Reservations)
                .Include(m => m.ParkingSpots)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (manager is null)
            {
                return NotFound("Parking Reservation Manager Not Found");
            }

            var convertedManager = _mapper.Map<ParkingReservationManagerDto>(manager);
            return Ok(convertedManager);
        }

        // Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateManager([FromRoute] string id, [FromBody] ParkingReservationManagerDto dto)
        {
            var manager = await _context.ParkingReservationManagers.FirstOrDefaultAsync(m => m.Id == id);
            if (manager is null)
            {
                return NotFound("Parking Reservation Manager Not Found");
            }

            manager.ManagerName = dto.ManagerName;
            manager.ManagerContact = dto.ManagerContact;

            await _context.SaveChangesAsync();
            return Ok("Parking Reservation Manager Updated Successfully");
        }

        // Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteManager([FromRoute] string id)
        {
            var manager = await _context.ParkingReservationManagers.FirstOrDefaultAsync(m => m.Id == id);
            if (manager is null)
            {
                return NotFound("Parking Reservation Manager Not Found");
            }

            _context.ParkingReservationManagers.Remove(manager);
            await _context.SaveChangesAsync();
            return Ok("Parking Reservation Manager Deleted");
        }
    }
}

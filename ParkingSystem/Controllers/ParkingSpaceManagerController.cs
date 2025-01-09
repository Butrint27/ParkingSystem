using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingSystem.Core.DbContext;
using ParkingSystem.Core.Entities;

namespace ParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingSpaceManagerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ParkingSpaceManagerController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateManager([FromBody] ParkingSpaceManagerDto dto)
        {
            var newManager = _mapper.Map<ParkingSpaceManager>(dto);

            await _context.ParkingSpaceManagers.AddAsync(newManager);
            await _context.SaveChangesAsync();

            return Ok("Parking Space Manager Created Successfully");
        }

        // Read All
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<ParkingSpaceManagerDto>>> GetAllManagers()
        {
            var managers = await _context.ParkingSpaceManagers
                .Include(m => m.ParkingSpaces)
                .ToListAsync();

            var convertedManagers = _mapper.Map<IEnumerable<ParkingSpaceManagerDto>>(managers);
            return Ok(convertedManagers);
        }

        // Read by ID
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ParkingSpaceManagerDto>> GetManagerById([FromRoute] int id)
        {
            var manager = await _context.ParkingSpaceManagers
                .Include(m => m.ParkingSpaces)
                .FirstOrDefaultAsync(m => m.id == id);

            if (manager is null)
            {
                return NotFound("Parking Space Manager Not Found");
            }

            var convertedManager = _mapper.Map<ParkingSpaceManagerDto>(manager);
            return Ok(convertedManager);
        }

        // Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateManager([FromRoute] int id, [FromBody] ParkingSpaceManagerDto dto)
        {
            var manager = await _context.ParkingSpaceManagers.FirstOrDefaultAsync(m => m.id == id);
            if (manager is null)
            {
                return NotFound("Parking Space Manager Not Found");
            }

            manager.status = dto.status;
            manager.pagesa = dto.pagesa;
            manager.kontakti = dto.kontakti;

            await _context.SaveChangesAsync();
            return Ok("Parking Space Manager Updated Successfully");
        }

        // Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteManager([FromRoute] int id)
        {
            var manager = await _context.ParkingSpaceManagers.FirstOrDefaultAsync(m => m.id == id);
            if (manager is null)
            {
                return NotFound("Parking Space Manager Not Found");
            }

            _context.ParkingSpaceManagers.Remove(manager);
            await _context.SaveChangesAsync();
            return Ok("Parking Space Manager Deleted");
        }
    }
}

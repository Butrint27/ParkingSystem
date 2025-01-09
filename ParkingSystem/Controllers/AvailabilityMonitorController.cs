using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingSystem.Core.DbContext;
using ParkingSystem.Core.Entities;

namespace ParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityMonitorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AvailabilityMonitorController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAvailabilityMonitor([FromBody] AvailabilityMonitorDto dto)
        {
            var newMonitor = _mapper.Map<AvailabilityMonitor>(dto);

            await _context.AvailabilityMonitors.AddAsync(newMonitor);
            await _context.SaveChangesAsync();

            return Ok("AvailabilityMonitor Created Successfully");
        }

        // Read All
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<AvailabilityMonitorDto>>> GetAllAvailabilityMonitors()
        {
            var monitors = await _context.AvailabilityMonitors
                .Include(m => m.ParkingSpaceManagers) // Include related ParkingSpaceManagers
                .Include(m => m.ParkingSpaces) // Include related ParkingSpaces
                .ToListAsync();

            var convertedMonitors = _mapper.Map<IEnumerable<AvailabilityMonitorDto>>(monitors);
            return Ok(convertedMonitors);
        }

        // Read by ID
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AvailabilityMonitorDto>> GetAvailabilityMonitorById([FromRoute] int id)
        {
            var monitor = await _context.AvailabilityMonitors
                .Include(m => m.ParkingSpaceManagers) // Include related ParkingSpaceManagers
                .Include(m => m.ParkingSpaces) // Include related ParkingSpaces
                .FirstOrDefaultAsync(m => m.MonitorId == id);

            if (monitor is null)
            {
                return NotFound("AvailabilityMonitor Not Found");
            }

            var convertedMonitor = _mapper.Map<AvailabilityMonitorDto>(monitor);
            return Ok(convertedMonitor);
        }

        // Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAvailabilityMonitor([FromRoute] int id, [FromBody] AvailabilityMonitorDto dto)
        {
            var monitor = await _context.AvailabilityMonitors.FirstOrDefaultAsync(m => m.MonitorId == id);
            if (monitor is null)
            {
                return NotFound("AvailabilityMonitor Not Found");
            }

            monitor.Status = dto.Status;
            monitor.LastCheckedTime = dto.LastCheckedTime;

            await _context.SaveChangesAsync();
            return Ok("AvailabilityMonitor Updated Successfully");
        }

        // Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAvailabilityMonitor([FromRoute] int id)
        {
            var monitor = await _context.AvailabilityMonitors.FirstOrDefaultAsync(m => m.MonitorId == id);
            if (monitor is null)
            {
                return NotFound("AvailabilityMonitor Not Found");
            }

            _context.AvailabilityMonitors.Remove(monitor);
            await _context.SaveChangesAsync();
            return Ok("AvailabilityMonitor Deleted");
        }
    }
}

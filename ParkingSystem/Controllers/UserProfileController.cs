using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingSystem.Core.DbContext;
using ParkingSystem.Core.Dtos.UserProfile;
using ParkingSystem.Core.Entities;

namespace ParkingSystem.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper { get; }

        public UserProfileController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUserProfile([FromBody] UserProfileDto dto)
        {
            UserProfile newProfile = _mapper.Map<UserProfile>(dto);
            await _context.UserProfiles.AddAsync(newProfile);
            await _context.SaveChangesAsync();
            return Ok("UserProfile Created Successfully");
        }

        // Read
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<UserProfileDto>>> GetAllUserProfiles()
        {
            var profiles = await _context.UserProfiles.ToListAsync();
            var convertedProfiles = _mapper.Map<IEnumerable<UserProfileDto>>(profiles);
            return Ok(convertedProfiles);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserProfileDto>> GetUserProfileById([FromRoute] int id)
        {
            var profile = await _context.UserProfiles.FirstOrDefaultAsync(q => q.UserId == id);
            if (profile == null)
                return NotFound("UserProfile Not Found");
            return Ok(_mapper.Map<UserProfileDto>(profile));
        }

        // Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUserProfile([FromRoute] int id, [FromBody] UserProfileDto dto)
        {
            var profile = await _context.UserProfiles.FirstOrDefaultAsync(q => q.UserId == id);
            if (profile == null)
                return NotFound("UserProfile Not Found");

            profile.address = dto.Address;
            profile.phoneNumber = dto.PhoneNumber;

            await _context.SaveChangesAsync();
            return Ok("UserProfile Updated Successfully");
        }

        // Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUserProfile([FromRoute] int id)
        {
            var profile = await _context.UserProfiles.FirstOrDefaultAsync(q => q.UserId == id);
            if (profile == null)
                return NotFound("UserProfile Not Found");

            _context.UserProfiles.Remove(profile);
            await _context.SaveChangesAsync();
            return Ok("UserProfile Deleted Successfully");
        }
    }
}

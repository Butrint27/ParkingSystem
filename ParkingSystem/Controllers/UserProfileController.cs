using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingSystem.Core.DbContext;
using ParkingSystem.Core.Dtos.UserProfile;
using ParkingSystem.Core.Entities;

namespace ParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

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
            var newUserProfile = _mapper.Map<UserProfile>(dto);

            await _context.UserProfiles.AddAsync(newUserProfile);
            await _context.SaveChangesAsync();

            return Ok("UserProfile Created Successfully");
        }

        // Read All
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<UserProfileDto>>> GetAllUserProfiles()
        {
            var userProfiles = await _context.UserProfiles
                .Include(up => up.User) // Include related User entity
                .ToListAsync();

            var convertedUserProfiles = _mapper.Map<IEnumerable<UserProfileDto>>(userProfiles);
            return Ok(convertedUserProfiles);
        }

        // Read by ID
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserProfileDto>> GetUserProfileById([FromRoute] int id)
        {
            var userProfile = await _context.UserProfiles
                .Include(up => up.User) // Include related User entity
                .FirstOrDefaultAsync(up => up.id == id);

            if (userProfile is null)
            {
                return NotFound("UserProfile Not Found");
            }

            var convertedUserProfile = _mapper.Map<UserProfileDto>(userProfile);
            return Ok(convertedUserProfile);
        }

        // Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUserProfile([FromRoute] int id, [FromBody] UserProfileDto dto)
        {
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.id == id);
            if (userProfile is null)
            {
                return NotFound("UserProfile Not Found");
            }

            userProfile.firstName = dto.FirstName;
            userProfile.lastName = dto.LastName;
            userProfile.address = dto.Address;
            userProfile.phoneNumber = dto.PhoneNumber;

            await _context.SaveChangesAsync();
            return Ok("UserProfile Updated Successfully");
        }

        // Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUserProfile([FromRoute] int id)
        {
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.id == id);
            if (userProfile is null)
            {
                return NotFound("UserProfile Not Found");
            }

            _context.UserProfiles.Remove(userProfile);
            await _context.SaveChangesAsync();
            return Ok("UserProfile Deleted");
        }
    }
}

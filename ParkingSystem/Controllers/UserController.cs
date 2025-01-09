using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingSystem.Core.DbContext;
using ParkingSystem.Core.Dtos.User;
using ParkingSystem.Core.Entities;

namespace ParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto dto)
        {
            var newUser = _mapper.Map<User>(dto);

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return Ok("User Created Successfully");
        }

        // Read All
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _context.Users
                .Include(u => u.UserProfile) // Include related UserProfile
                .ToListAsync();

            var convertedUsers = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(convertedUsers);
        }

        // Read by ID
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById([FromRoute] int id)
        {
            var user = await _context.Users
                .Include(u => u.UserProfile) // Include related UserProfile
                .FirstOrDefaultAsync(u => u.id == id);

            if (user is null)
            {
                return NotFound("User Not Found");
            }

            var convertedUser = _mapper.Map<UserDto>(user);
            return Ok(convertedUser);
        }

        // Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UserDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.id == id);
            if (user is null)
            {
                return NotFound("User Not Found");
            }

            user.username = dto.Username;
            user.email = dto.Email;
            user.role = dto.Role;

            await _context.SaveChangesAsync();
            return Ok("User Updated Successfully");
        }

        // Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.id == id);
            if (user is null)
            {
                return NotFound("User Not Found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok("User Deleted");
        }
    }
}

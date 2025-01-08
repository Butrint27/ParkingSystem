using ParkingSystem.Core.Dtos.UserProfile;

namespace ParkingSystem.Core.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public UserProfileDto UserProfile { get; set; }
    }
}

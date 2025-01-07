using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.Core.Entities
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public String username { get; set; }
        public String email { get; set; }
        public String role { get; set; }

        //lidhja nje me nje
        public UserProfile UserProfile { get; set; }
    }
}

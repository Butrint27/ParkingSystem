using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.Core.Entities
{
    public class Authentication
    {
        [Key]
        public int id { get; set; }
        public String username { get; set; }
        public String password { get; set; }
        public String token { get; set; }

        //Lidhja
        public int UserId { get; set; }
        public User User { get; set; }
    }
}

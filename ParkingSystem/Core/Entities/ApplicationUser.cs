using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingSystem.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [NotMapped]
        public IList<string> Roles { get; set; }

        // Add Email and UserName properties here
        public string Email { get; set; }
        public string UserName { get; set; }

        // Assuming you want to use a SecurityStamp property as well
        public string SecurityStamp { get; set; }
    }
}

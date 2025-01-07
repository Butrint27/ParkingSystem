namespace ParkingSystem.Core.Entities
{
    public class UserProfile
    {
        public int id { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String address { get; set; }
        public String phoneNumber { get; set; }

        //lidhje 
        public int UserId { get; set; }
        public User User { get; set; }
    }
}

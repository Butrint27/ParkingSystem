using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParkingSystem.Core.Entities;

namespace ParkingSystem.Core.DbContext
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
        
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile>UserProfiles { get; set; }
        public DbSet<Authentication>Authentications { get; set; }
        public DbSet<Payment>Payments { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<ParkingSpace> ParkingSpaces { get; set; }
        public DbSet<ParkingSpaceManager> ParkingSpaceManagers { get; set; }
        public DbSet<AvailabilityMonitor> AvailabilityMonitors { get; set;}
        public DbSet<ParkingSpot> ParkingSpots { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ParkingReservationManager> ParkingReservationManagers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Lidhja midis User dhe UserProfile (One-to-One)
            builder.Entity<User>()
                .HasOne(u => u.UserProfile)
                .WithOne(up => up.User)
                .HasForeignKey<UserProfile>(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Lidhja midis User dhe Authentication (One-to-One)
            builder.Entity<User>()
                .HasOne(u => u.Authentication)
                .WithOne(a => a.User)
                .HasForeignKey<Authentication>(a => a.username)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

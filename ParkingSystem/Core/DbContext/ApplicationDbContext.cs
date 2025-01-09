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
        public DbSet<Log> Logs { get; set; }
        public DbSet<Message> Messages { get; set; }
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

            builder.Entity<Reservation>()
        .HasOne(r => r.ParkingSpot)
        .WithMany(ps => ps.Reservations)
        .HasForeignKey(r => r.ParkingSpotId)
        .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ParkingReservationManager>()
        .HasOne<Reservation>()
        .WithMany(r => r.ParkingReservationManagers)
        .HasForeignKey(prm => prm.Id)
        .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Invoice>()
        .HasOne(i => i.Payment) // Një Invoice ka një Payment
        .WithOne()              // Një Payment ka një Invoice
        .HasForeignKey<Invoice>(i => i.PaymentId) // Çelësi i huaj në Invoice është PaymentId
        .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Payment>()
        .HasOne(p => p.paymentMethod) // Një Payment ka një PaymentMethod
        .WithMany(pm => pm.Payments)  // Një PaymentMethod ka shumë Payment
        .HasForeignKey(p => p.PaymentId) // Çelësi i huaj në Payment është PaymentId
        .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ParkingSpace>()
        .HasOne<ParkingSpaceManager>()
        .WithMany(psm => psm.ParkingSpaces)
        .HasForeignKey(ps => ps.Id)
        .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ParkingSpace>()
        .HasOne<AvailabilityMonitor>()
        .WithMany(am => am.ParkingSpaces)
        .HasForeignKey(ps => ps.Id)
        .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

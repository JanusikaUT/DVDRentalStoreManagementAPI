using DVD_RENTAL_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DVD_RENTAL_API.Data
{
    public class DVDContext : DbContext
    {
        public DVDContext(DbContextOptions<DVDContext> options) : base(options) { }

        public DbSet<DVD> DVDs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.DVD)
                .WithMany(d => d.Rentals)
                .HasForeignKey(r => r.DVDId);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Rental)
                .WithMany()
                .HasForeignKey(n => n.RentalId);
        }
    }
}

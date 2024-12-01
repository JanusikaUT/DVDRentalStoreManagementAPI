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
    }
}

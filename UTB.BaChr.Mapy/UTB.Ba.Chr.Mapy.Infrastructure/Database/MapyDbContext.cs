using Microsoft.EntityFrameworkCore;
using UTB.BaChr.Mapy.Domain.Entities;
using UTB.BaChr.Mapy.Infrastructure.Database.Seeding; // Add this namespace

namespace UTB.BaChr.Mapy.Infrastructure.Database
{
    public class MapyDbContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<User> Users { get; set; }

        public MapyDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        // --- ADD THIS METHOD ---
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Call the LocationInit seeder
            LocationInit locationInit = new LocationInit();
            modelBuilder.Entity<Location>().HasData(locationInit.GetLocations());
        }
    }
}
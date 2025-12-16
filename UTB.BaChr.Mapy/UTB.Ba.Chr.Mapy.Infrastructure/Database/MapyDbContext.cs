using Microsoft.EntityFrameworkCore;
using UTB.BaChr.Mapy.Domain.Entities;
using UTB.BaChr.Mapy.Infrastructure.Database.Seeding;

namespace UTB.BaChr.Mapy.Infrastructure.Database
{
    public class MapyDbContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public MapyDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Seeding Lokací
            LocationInit locationInit = new LocationInit();
            modelBuilder.Entity<Location>().HasData(locationInit.GetLocations());

            // 2. NOVÉ: Seeding Uživatelů (Admin + Klient)
            UserInit userInit = new UserInit();
            modelBuilder.Entity<User>().HasData(userInit.GetUsers());
        }
    }
}
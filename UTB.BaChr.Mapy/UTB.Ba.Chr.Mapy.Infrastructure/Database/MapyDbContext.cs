using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTB.BaChr.Mapy.Domain.Entities;

namespace UTB.BaChr.Mapy.Infrastructure.Database
{

    public class MapyDbContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<User> Users { get; set; }

        public MapyDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}

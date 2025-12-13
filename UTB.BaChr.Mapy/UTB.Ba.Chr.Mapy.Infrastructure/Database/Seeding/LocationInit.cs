using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTB.BaChr.Mapy.Domain.Entities;

namespace UTB.BaChr.Mapy.Infrastructure.Database.Seeding
{
    public class LocationInit
    {

        public List<Location> GenerateLocation()
        {
            List<Location> locations = new List<Location>();

            var location1 = new Location()
            {
                Id = 1,
                Name = "Two Forks Lookout",
                Description = "Lookout",
                MapX = 0.5,
                MapY = 0.5
            };

            locations.Add(location1);

            return locations;
        }
    }
}

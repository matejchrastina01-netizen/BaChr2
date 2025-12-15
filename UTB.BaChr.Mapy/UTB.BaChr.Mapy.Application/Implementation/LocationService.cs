using System.Collections.Generic;
using System.Linq;
using UTB.BaChr.Mapy.Application.Abstraction;
using UTB.BaChr.Mapy.Domain.Entities;
using UTB.BaChr.Mapy.Infrastructure.Database;

namespace UTB.BaChr.Mapy.Application.Implementation
{
    public class LocationService : ILocationService
    {
        private readonly MapyDbContext _mapyDbContext;

        public LocationService(MapyDbContext mapyDbContext)
        {
            _mapyDbContext = mapyDbContext;
        }

        public IList<Location> Select()
        {
            return _mapyDbContext.Locations.ToList();
        }

        public Location? GetById(int id)
        {
            return _mapyDbContext.Locations.FirstOrDefault(l => l.Id == id);
        }

        public void Create(Location location)
        {
            _mapyDbContext.Locations.Add(location);
            _mapyDbContext.SaveChanges();
        }

        public void Update(Location location)
        {
            _mapyDbContext.Locations.Update(location);
            _mapyDbContext.SaveChanges();
        }

        public bool Delete(int id)
        {
            var location = _mapyDbContext.Locations.Find(id);
            if (location != null)
            {
                _mapyDbContext.Locations.Remove(location);
                _mapyDbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
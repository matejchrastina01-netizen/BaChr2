using System.Collections.Generic;
using UTB.BaChr.Mapy.Domain.Entities;

namespace UTB.BaChr.Mapy.Application.Abstraction
{
    public interface ILocationService
    {
        IList<Location> Select();
        // Tyto metody tam necháme pro budoucí použití, i když je teď mapa přímo nepotřebuje
        Location? GetById(int id);
        void Create(Location location);
        void Update(Location location);
        bool Delete(int id);
    }
}
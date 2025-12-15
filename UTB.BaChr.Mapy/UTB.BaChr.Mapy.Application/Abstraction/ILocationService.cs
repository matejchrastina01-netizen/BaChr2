using System.Collections.Generic;
using UTB.BaChr.Mapy.Domain.Entities;

namespace UTB.BaChr.Mapy.Application.Abstraction
{
    public interface ILocationService
    {
        IList<Location> Select();
        Location? GetById(int id);

        // Nové metody pro detailní zobrazení s Include
        Location? GetByIdWithDetails(int id);

        void Create(Location location);
        void Update(Location location);
        bool Delete(int id);

        // Práce s obsahem
        void AddComment(Comment comment);
        bool DeleteComment(int commentId, int userId, bool isAdmin);

        void AddPhoto(Photo photo);
        bool DeletePhoto(int photoId, int userId, bool isAdmin);
    }
}
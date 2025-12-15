using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using UTB.BaChr.Mapy.Application.Abstraction;
using UTB.BaChr.Mapy.Domain.Entities;
using UTB.BaChr.Mapy.Infrastructure.Database;

namespace UTB.BaChr.Mapy.Application.Implementation
{
    public class LocationService : ILocationService
    {
        private readonly MapyDbContext _context;

        public LocationService(MapyDbContext context)
        {
            _context = context;
        }

        public IList<Location> Select()
        {
            return _context.Locations.ToList();
        }

        public Location? GetById(int id)
        {
            return _context.Locations.Find(id);
        }

        // Načte lokaci i s fotkami a komentáři (a autory komentářů)
        public Location? GetByIdWithDetails(int id)
        {
            return _context.Locations
                .Include(l => l.Photos)
                .Include(l => l.Comments)
                    .ThenInclude(c => c.User) // Abychom viděli jméno autora komentáře
                .FirstOrDefault(l => l.Id == id);
        }

        public void Create(Location location)
        {
            _context.Locations.Add(location);
            _context.SaveChanges();
        }

        public void Update(Location location)
        {
            _context.Locations.Update(location);
            _context.SaveChanges();
        }

        public bool Delete(int id)
        {
            var location = _context.Locations.Find(id);
            if (location != null)
            {
                _context.Locations.Remove(location);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        // --- KOMENTÁŘE ---
        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public bool DeleteComment(int commentId, int userId, bool isAdmin)
        {
            var comment = _context.Comments.Find(commentId);
            if (comment == null) return false;

            // Logika: Admin může vše, Klient jen své
            if (isAdmin || comment.UserId == userId)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        // --- FOTKY ---
        public void AddPhoto(Photo photo)
        {
            _context.Photos.Add(photo);
            _context.SaveChanges();
        }

        public bool DeletePhoto(int photoId, int userId, bool isAdmin)
        {
            var photo = _context.Photos.Find(photoId);
            if (photo == null) return false;

            if (isAdmin || photo.UserId == userId)
            {
                _context.Photos.Remove(photo);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
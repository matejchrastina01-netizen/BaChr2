using UTB.BaChr.Mapy.Domain.Entities;

namespace UTB.BaChr.Mapy.Application.Abstraction
{
    public interface ISecurityService
    {
        string HashPassword(User user, string password);
        bool VerifyPassword(User user, string hashedPassword, string providedPassword);
    }
}
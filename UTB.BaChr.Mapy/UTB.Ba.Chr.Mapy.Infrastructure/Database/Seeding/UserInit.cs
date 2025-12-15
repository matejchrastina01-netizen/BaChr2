using Microsoft.AspNetCore.Identity; // Nutné pro PasswordHasher
using UTB.BaChr.Mapy.Domain.Entities;

namespace UTB.BaChr.Mapy.Infrastructure.Database.Seeding
{
    internal class UserInit
    {
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            // Vytvoříme instanci hasheru přímo zde (pro seeding nepotřebujeme service)
            var passwordHasher = new PasswordHasher<User>();

            // 1. ADMIN ÚČET
            var adminUser = new User
            {
                Id = 1,
                Email = "admin@admin.cz",
                Name = "Hlavní Administrátor",
                Role = "Admin"
            };
            // Ručně zahashujeme heslo "admin"
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "admin");
            users.Add(adminUser);

            // 2. KLIENT ÚČET
            var clientUser = new User
            {
                Id = 2,
                Email = "klient@klient.cz",
                Name = "Testovací Klient",
                Role = "User"
            };
            // Ručně zahashujeme heslo "klient"
            clientUser.PasswordHash = passwordHasher.HashPassword(clientUser, "klient");
            users.Add(clientUser);

            return users;
        }
    }
}
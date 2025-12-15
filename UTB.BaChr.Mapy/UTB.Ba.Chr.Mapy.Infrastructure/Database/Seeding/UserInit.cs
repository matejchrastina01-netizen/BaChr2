using UTB.BaChr.Mapy.Domain.Entities;

namespace UTB.BaChr.Mapy.Infrastructure.Database.Seeding
{
    internal class UserInit
    {
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            // 1. ADMIN ÚČET
            users.Add(new User
            {
                Id = 1, // Pevné ID je důležité
                Email = "admin@admin.cz",
                Name = "Hlavní Administrátor",
                PasswordHash = "admin", // Heslo
                Role = "Admin"
            });

            // 2. KLIENT ÚČET (pro testování)
            users.Add(new User
            {
                Id = 2,
                Email = "klient@klient.cz",
                Name = "Testovací Klient",
                PasswordHash = "klient", // Heslo
                Role = "User" // Váš systém používá "User" jako roli pro klienta
            });

            return users;
        }
    }
}
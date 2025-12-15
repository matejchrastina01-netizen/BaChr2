using UTB.BaChr.Mapy.Domain.Entities;

namespace UTB.BaChr.Mapy.Infrastructure.Database.Seeding
{
    internal class LocationInit
    {
        public List<Location> GetLocations()
        {
            List<Location> locations = new List<Location>();

            locations.Add(new Location
            {
                Id = 1,
                Name = "Two Forks Tower",
                Description = "Henryho strážní věž. Domov daleko od domova.",
                MapX = 1268,
                MapY = 1395
            });

            locations.Add(new Location
            {
                Id = 2,
                Name = "Delilah's Tower",
                Description = "Thorofare Lookout. Věž, kde slouží Delilah.",
                MapX = 1132,
                MapY = 225
            });

            locations.Add(new Location
            {
                Id = 3,
                Name = "Jonesy Lake",
                Description = "Klidné jezero ideální pro rybaření a přemýšlení.",
                MapX = 275,
                MapY = 1206
            });

            locations.Add(new Location
            {
                Id = 4,
                Name = "Beartooth Point",
                Description = "Místo s nádherným výhledem na celé údolí.",
                MapX = 1283,
                MapY = 323
            });

            locations.Add(new Location
            {
                Id = 5,
                Name = "Wapiti Station",
                Description = "Oplocená výzkumná stanice. Vstup zakázán.",
                MapX = 562,
                MapY = 770
            });

            locations.Add(new Location
            {
                Id = 6,
                Name = "Cottonwood Creek",
                Description = "Potok protékající jižní částí lesa.",
                MapX = 1013,
                MapY = 1787
            });

            return locations;
        }
    }
}
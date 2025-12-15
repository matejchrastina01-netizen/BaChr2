using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UTB.BaChr.Mapy.Domain.Entities
{
    [Table(nameof(Location))]
    public class Location
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? MapX { get; set; }
        public double? MapY { get; set; }

        // Vazby
        public virtual ICollection<Photo>? Photos { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
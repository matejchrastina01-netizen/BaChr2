using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

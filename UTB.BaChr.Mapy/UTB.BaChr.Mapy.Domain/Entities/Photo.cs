using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTB.BaChr.Mapy.Domain.Entities
{
    class Photo
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public Guid? UploadedById { get; set; }
        public int? LocationId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

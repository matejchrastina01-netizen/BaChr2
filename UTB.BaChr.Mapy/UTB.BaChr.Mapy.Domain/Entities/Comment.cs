using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTB.BaChr.Mapy.Domain.Entities
{
    class Comment
    {
        public int Id { get; set; }
        public Guid? PhotoId { get; set; }
        public Guid? AuthorId  { get; set; }
        public string? Text { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTB.BaChr.Mapy.Domain.Entities
{
    class PhotoTag
    {
        public Guid? PhotoId { get; set; }
        public int? TagId { get; set; }
    }
}

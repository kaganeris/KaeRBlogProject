using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class Like : IBaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}

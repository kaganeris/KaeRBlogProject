using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class Reply: IBaseEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Guid AppUserId { get; set; }
        public int CommentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get; set; }
        public AppUser AppUser { get; set; }
        public Comment Comment { get; set; }
    }
}

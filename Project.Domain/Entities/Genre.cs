using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class Genre : IBaseEntity
    {
        public Genre()
        {
            Posts = new List<Post>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get; set; }

        // Navigation Properties

        public List<Post> Posts { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class Author : IBaseEntity
    {
        public Author()
        {
            Posts = new List<Post>();
        }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get; set; }
        public Guid AppUserId { get; set; }

        // Navigation Properties
        public AppUser AppUser { get; set; }
        public List<Post> Posts { get; set; }
    }
}

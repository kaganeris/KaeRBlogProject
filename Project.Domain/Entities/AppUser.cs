using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>, IBaseEntity
    {
        public AppUser()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
        }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [NotMapped]
        public string FullName => FirstName + " " + LastName;
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public string About { get; set; }
        public string ImagePath { get; set; }

        [NotMapped] // DB'de yer almayacak
        public IFormFile UploadPath { get; set; }
        public int ConfirmCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get; set; }

        // Navigation Prop
        public List<Comment> Comments { get; set; }
        public List<Reply> Replies { get; set; }
        public List<Like> Likes { get; set; }
        public Author? Author { get; set; }
    }
}

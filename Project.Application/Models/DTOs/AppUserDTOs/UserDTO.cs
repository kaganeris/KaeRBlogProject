using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Models.DTOs.AppUserDTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int UserType { get; set; }
        public int PostCount { get; set; }
        public int ReplyCount { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public Status Status { get; set; }
    }
}

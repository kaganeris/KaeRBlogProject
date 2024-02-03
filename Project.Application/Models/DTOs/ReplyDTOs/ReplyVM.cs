using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Models.DTOs.ReplyDTOs
{
    public class ReplyVM
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string AppUserImagePath { get; set; }
        public string AppUserFullName { get; set; }
        public string CommentName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get; set; }
    }
}

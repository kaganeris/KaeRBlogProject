using Project.Application.Models.DTOs.ReplyDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Models.DTOs.CommentDTOs
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public string AppUserFullName { get; set; }
        public string AppUserImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<ReplyDTO> Replies { get; set; }

    }
}

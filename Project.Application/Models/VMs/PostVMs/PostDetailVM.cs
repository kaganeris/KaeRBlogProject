using Project.Application.Models.DTOs.CommentDTOs;
using Project.Application.Models.DTOs.ReplyDTOs;
using Project.Application.Models.VMs.AuthorVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Models.VMs.PostVMs
{
    public class PostDetailVM
    {
        public int PostId { get; set; }
        public Guid AppUserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public string GenreName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ClickCount { get; set; }
        public int LikeCount { get; set; }
        public bool IsLiked { get; set; }
        public List<CommentDTO> Comments { get; set; }

    }
}

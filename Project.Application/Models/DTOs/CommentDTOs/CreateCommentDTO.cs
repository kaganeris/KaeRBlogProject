using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Models.DTOs.CommentDTOs
{
    public class CreateCommentDTO
    {
        public string Content { get; set; }
        public Guid? AppUserId { get; set; }
        public int PostId { get; set; }
    }
}

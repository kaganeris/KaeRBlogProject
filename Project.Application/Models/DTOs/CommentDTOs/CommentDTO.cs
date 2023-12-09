using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Models.DTOs.CommentDTOs
{
    public class CommentDTO
    {
        public string Content { get; set; }
        public string AppUserFullName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Replies { get; set; }

    }
}

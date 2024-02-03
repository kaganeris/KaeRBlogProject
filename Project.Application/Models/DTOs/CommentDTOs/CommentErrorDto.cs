using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Models.DTOs.CommentDTOs
{
    public class CommentErrorDto
    {
        public string Error { get; set; }
        public Guid userID { get; set; }
    }
}

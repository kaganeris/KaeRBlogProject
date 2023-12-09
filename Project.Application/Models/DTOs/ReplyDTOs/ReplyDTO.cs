using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Models.DTOs.ReplyDTOs
{
    public class ReplyDTO
    {
        public string Content { get; set; }
        public string AppUserImagePath { get; set; }
        public string AppUserFullName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

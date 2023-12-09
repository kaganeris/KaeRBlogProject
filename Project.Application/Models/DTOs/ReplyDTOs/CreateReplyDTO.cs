﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Models.DTOs.ReplyDTOs
{
    public class CreateReplyDTO
    {
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CommentId { get; set; }
        public Guid AppUserId { get; set; }
    }
}

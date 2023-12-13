using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Models.DTOs.LikeDTOs
{
    public class CreateLikeDTO
    {
        public int PostId { get; set; }
        public Guid AppUserId { get; set; }
    }
}

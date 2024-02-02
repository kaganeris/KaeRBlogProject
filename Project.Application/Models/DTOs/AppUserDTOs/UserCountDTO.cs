using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Models.DTOs.AppUserDTOs
{
    public class UserCountDTO
    {
        public int TotalUserCount { get; set; }
        public int AuthorCount { get; set; }
        public int NormalUserCount { get; set; }
    }
}

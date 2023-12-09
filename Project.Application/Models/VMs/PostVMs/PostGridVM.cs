using Project.Application.Models.DTOs.AuthorDTOs;
using Project.Application.Models.VMs.GenreVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Models.VMs.PostVMs
{
    public class PostGridVM
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AuthorFullName { get; set; }
        public string AuthorPhoto { get; set; }
        public string GenreName { get; set; }
        public bool IsLiked { get; set; }
    }
}

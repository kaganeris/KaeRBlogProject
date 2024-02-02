
using Project.Application.Models.VMs.GenreVMs;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Models.DTOs.PostDTOs
{
    public class PostListDTO
    {
        public int PostId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorPhoto { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public string GenreName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get; set; }
    }
}

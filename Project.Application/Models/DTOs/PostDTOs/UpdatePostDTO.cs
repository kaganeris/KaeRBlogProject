using Microsoft.AspNetCore.Http;
using Project.Application.Extensions;
using Project.Application.Models.VMs.AuthorVMs;
using Project.Application.Models.VMs.GenreVMs;
using Project.Domain.Entities;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Models.DTOs.PostDTOs
{
    public class UpdatePostDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ImagePath { get; set; }

        [PictureFileExtension]
        public IFormFile? UploadPath { get; set; }
        public int GenreID { get; set; }
        public List<GenreVM>? Genres { get; set; }
    }
}

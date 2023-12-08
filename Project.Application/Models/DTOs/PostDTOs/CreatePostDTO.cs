using Microsoft.AspNetCore.Http;
using Project.Application.Extensions;
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
    public class CreatePostDTO
    {
        [Required(ErrorMessage = "Başlık boş geçilemez!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "İçerik boş geçilemez!")]
        public string Content { get; set; }
        public string? ImagePath { get; set; }

        [PictureFileExtension]
        public IFormFile UploadPath { get; set; }
        public int? AuthorId { get; set; }
        [Required(ErrorMessage = "Kategori seçmelisiniz!")]
        public int? GenreId { get; set; }
        public List<GenreVM>? Genres { get; set; }
    }
}

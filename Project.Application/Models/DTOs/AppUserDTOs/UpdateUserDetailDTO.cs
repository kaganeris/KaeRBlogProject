﻿using Microsoft.AspNetCore.Http;
using Project.Application.Extensions;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Models.DTOs.AppUserDTOs
{
    public class UpdateUserDetailDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Ad boş geçilemez!")]
        [MinLength(6, ErrorMessage = "Ad minimum 3 karakterden oluşmalıdır!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad boş geçilemez!")]
        [MinLength(6, ErrorMessage = "Soyad minimum 3 karakterden oluşmalıdır!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Doğum Tarihi boş geçilemez!")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Cinsiyet boş geçilemez!")]
        public Gender Gender { get; set; }
        public string? ImagePath { get; set; }

        [PictureFileExtension]
        public IFormFile UploadPath { get; set; }
    }
}

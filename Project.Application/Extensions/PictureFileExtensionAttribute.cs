using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Extensions
{
    public class PictureFileExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IFormFile file = value as IFormFile; // Formdan gelen dosyayı al.

            if (file != null)
            {
                // Dosyanın uzantısını al
                var extension = Path.GetExtension(file.FileName).ToLower(); // JPEG,PNG => jpeg, png

                string[] extensions = { ".jpg", ".jpeg", ".png" };

                bool result = extensions.Any(x => x.EndsWith(extension));

                if (!result)
                {
                    return new ValidationResult("Fotoğraf için kabul edilen formatlar 'jpg','png','jpeg'dir.");
                }
            }
            return ValidationResult.Success; // Validasyon başarılı
        }
    }
}

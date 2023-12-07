using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project.Application.Models.DTOs.AppUserDTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez!")]
        [MinLength(6, ErrorMessage = "Kullanıcı adı minimum 6 karakterden oluşmalıdır!")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email boş geçilemez!")]
        [MinLength(8, ErrorMessage = "Email minimum 8 karakterden oluşmalıdır!")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail Adresi")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre boş geçilemez!")]
        [MinLength(8, ErrorMessage = "Şifre minimum 8 karakterden oluşmalıdır!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrarı")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "Şifre Tekrarı şifre ile aynı olmalıdır!")]
        public string ConfirmPassword { get; set; }
        public string ImagePath => "/images/profilePhotos/defUserPhoto.jpg";
    }
}

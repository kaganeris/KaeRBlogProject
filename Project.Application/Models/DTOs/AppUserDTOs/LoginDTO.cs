using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Models.DTOs.AppUserDTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Kullanıcı Adı boş geçilemez!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre boş geçilemez!")]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}

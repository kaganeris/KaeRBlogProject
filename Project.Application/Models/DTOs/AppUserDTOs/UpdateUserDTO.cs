using Microsoft.AspNetCore.Http;
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
    public class UpdateUserDTO
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Must to type UserName!")]
        [MinLength(6, ErrorMessage = "Minimum length is 6!")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Must to type Email!")]
        [MinLength(8, ErrorMessage = "Minimum length is 8!")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Must to type UserName!")]
        [MinLength(8, ErrorMessage = "Minimum length is 8!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Comfirm Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Models.DTOs.GenreDTOs
{
    public class CreateGenreDTO
    {
        [Required(ErrorMessage = "Kategori Adı boş geçilemez!")]
        public string Name { get; set; }
    }
}

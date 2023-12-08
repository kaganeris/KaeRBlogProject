using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Models.DTOs.GenreDTOs
{
    public class UpdateGenreDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı boş geçilemez!")]
        public string Name { get; set; }
    }
}

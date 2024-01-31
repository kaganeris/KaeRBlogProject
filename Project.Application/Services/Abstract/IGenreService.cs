using Project.Application.Models.DTOs.GenreDTOs;
using Project.Application.Models.VMs.GenreVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Services.Abstract
{
    public interface IGenreService
    {
        Task<List<GenreVM>> GetGenreList();
        Task<UpdateGenreDTO> GetGenreById(int id);
        Task<GenreVM> GetGenreByName(string genreName);

        Task<bool> CreateGenre(CreateGenreDTO createGenreDTO);
        Task<bool> UpdateGenre(UpdateGenreDTO updateGenreDTO);
        Task<bool> DeleteGenre(int id);
    }
}

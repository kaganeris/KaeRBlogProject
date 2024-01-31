using AutoMapper;
using Project.Application.Models.DTOs.GenreDTOs;
using Project.Application.Models.VMs.GenreVMs;
using Project.Application.Services.Abstract;
using Project.Domain.Entities;
using Project.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Services.Concrete
{
    public class GenreManager : IGenreService
    {
        private readonly IGenreRepository genreRepository;
        private readonly IMapper mapper;

        public GenreManager(IGenreRepository genreRepository, IMapper mapper)
        {
            this.genreRepository = genreRepository;
            this.mapper = mapper;
        }
        public async Task<bool> CreateGenre(CreateGenreDTO createGenreDTO)
        {
            if (createGenreDTO == null)
                return false;
            else
            {
                Genre genre = mapper.Map<Genre>(createGenreDTO);
                return await genreRepository.Create(genre);
            }
        }

        public async Task<bool> DeleteGenre(int id)
        {
            if (id <= 0)
                return false;
            else
            {
                Genre genre = await genreRepository.GetDefault(x => x.Id == id);
                if (genre == null)
                    return false;
                else
                {
                    bool result = await genreRepository.Delete(genre);
                    return result;

                }
            }

        }

        public async Task<UpdateGenreDTO> GetGenreById(int id)
        {
            if (id <= 0)
                return null;
            else
            {
                Genre genre = await genreRepository.GetDefault(x => x.Id == id);
                UpdateGenreDTO updateGenreDTO = mapper.Map<UpdateGenreDTO>(genre);
                return updateGenreDTO;
            }
        }

        public async Task<GenreVM> GetGenreByName(string genreName)
        {
            if(genreName == null)
            {
                return null;
            }
            else
            {
                GenreVM genreVM = await genreRepository.GetFilteredFirstOrDefault(
                    select: x => new GenreVM
                    {
                        Name = x.Name,
                        Id = x.Id
                    },
                    where: x => x.Name == genreName);

                return genreVM;
            }
        }

        public async Task<List<GenreVM>> GetGenreList()
        {
            return await genreRepository.GetFilteredList(
                select: x => new GenreVM
                {
                    Id = x.Id,
                    Name = x.Name,
                },
                where: x => x.Status == Domain.Enums.Status.Active
                );
        }

        public async Task<bool> UpdateGenre(UpdateGenreDTO updateGenreDTO)
        {
            if (updateGenreDTO == null) return false;
            else
            {
                Genre genre = await genreRepository.GetDefault(x => x.Id == updateGenreDTO.Id);
                genre = mapper.Map(updateGenreDTO, genre);
                return await genreRepository.Update(genre);
            }
        }
    }
}

using AutoMapper;
using Project.Application.Models.DTOs.AuthorDTOs;
using Project.Application.Models.VMs.AuthorVMs;
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
    public class AuthorManager : IAuthorService
    {
        private readonly IAuthorRepository authorRepository;
        private readonly IMapper mapper;

        public AuthorManager(IAuthorRepository authorRepository, IMapper mapper)
        {
            this.authorRepository = authorRepository;
            this.mapper = mapper;
        }
        public async Task<bool> CreateAuthor(CreateAuthorDTO createAuthorDTO)
        {
            if (createAuthorDTO == null)
                return false;
            else
            {
                Author author = mapper.Map<Author>(createAuthorDTO);
                await authorRepository.Create(author);
                return true;
            }
        }

        public Task<bool> DeleteAuthor(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateAuthorDTO> GetAuthorById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetAuthorIdByUserId(Guid userId)
        {
            Author author = await authorRepository.GetDefault(x => x.AppUserId == userId);
            if(author == null)
            {
                return 0;
            }
            else
            {
                return author.Id;
            }
        }

        public Task<List<AuthorDetailVM>> GetDetailAuthorList()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAuthor(UpdateAuthorDTO updateAuthorDTO)
        {
            throw new NotImplementedException();
        }
    }
}

using Project.Application.Models.DTOs.AuthorDTOs;
using Project.Application.Models.VMs.AuthorVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Services.Abstract
{
    public interface IAuthorService
    {
        Task<List<AuthorDetailVM>> GetDetailAuthorList();
        Task<UpdateAuthorDTO> GetAuthorById(int id);
        Task<int> GetAuthorIdByUserId(Guid userId);

        Task<bool> CreateAuthor(CreateAuthorDTO createAuthorDTO);
        Task<bool> UpdateAuthor(UpdateAuthorDTO updateAuthorDTO);
        Task<bool> DeleteAuthor(int id);
    }
}

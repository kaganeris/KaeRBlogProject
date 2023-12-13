using Project.Application.Models.DTOs.LikeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Services.Abstract
{
    public interface ILikeService
    {
        Task<bool> CreateLike(CreateLikeDTO createLikeDTO);
        Task<bool> UpdateLike(UpdateLikeDTO updateLikeDTO);
        Task<bool> DeleteLike(int id);

        Task<int> GetLikeId(int postId, Guid appUserId);
    }
}

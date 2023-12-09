using Project.Application.Models.DTOs.CommentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Services.Abstract
{
    public interface ICommentService
    {
        Task<bool> CreateComment(CreateCommentDTO createCommentDTO);
        Task<bool> UpdateComment(UpdateCommentDTO updateCommentDTO);
        Task<bool> DeleteComment(int id);
    }
}

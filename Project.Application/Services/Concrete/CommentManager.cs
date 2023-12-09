using AutoMapper;
using Project.Application.Models.DTOs.CommentDTOs;
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
    public class CommentManager : ICommentService
    {
        private readonly ICommentRepository commentRepository;
        private readonly IMapper mapper;

        public CommentManager(ICommentRepository commentRepository, IMapper mapper)
        {
            this.commentRepository = commentRepository;
            this.mapper = mapper;
        }
        public async Task<bool> CreateComment(CreateCommentDTO createCommentDTO)
        {
            if (createCommentDTO == null)
            {
                return false;
            }
            else
            {
                Comment comment = mapper.Map<Comment>(createCommentDTO);
                return await commentRepository.Create(comment);
            }
        }

        public async Task<bool> DeleteComment(int id)
        {
            if (id <= 0)
            {
                return false;
            }
            else
            {
                Comment deleteComment = await commentRepository.GetDefault(x => x.Id == id);
                if (deleteComment == null)
                {
                    return false;
                }
                else
                {
                    return await commentRepository.Delete(deleteComment);
                }
            }
        }

        public async Task<bool> UpdateComment(UpdateCommentDTO updateCommentDTO)
        {
            if (updateCommentDTO == null)
            {
                return false;
            }
            else
            {
                Comment updateComment = await commentRepository.GetDefault(x => x.Id == updateCommentDTO.Id);
                if (updateComment == null)
                {
                    return false;
                }
                else
                {
                    updateComment = mapper.Map(updateCommentDTO, updateComment);
                    return await commentRepository.Update(updateComment);
                }

            }
        }
    }
}

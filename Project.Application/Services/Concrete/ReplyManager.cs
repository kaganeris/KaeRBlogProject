using AutoMapper;
using Project.Application.Models.DTOs.ReplyDTOs;
using Project.Application.Services.Abstract;
using Project.Domain.Entities;
using Project.Domain.Repositories;
using Project.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Services.Concrete
{
    public class ReplyManager : IReplyService
    {
        private readonly IReplyRepository replyRepository;
        private readonly IMapper mapper;

        public ReplyManager(IReplyRepository replyRepository,IMapper mapper)
        {
            this.replyRepository = replyRepository;
            this.mapper = mapper;
        }
        public async Task<bool> CreateReply(CreateReplyDTO createReplyDTO)
        {
            if (createReplyDTO == null)
            {
                return false;
            }
            else
            {
                Reply comment = mapper.Map<Reply>(createReplyDTO);
                return await replyRepository.Create(comment);
            }
        }

        public async Task<bool> DeleteReply(int id)
        {
            if (id <= 0)
            {
                return false;
            }
            else
            {
                Reply deleteReply = await replyRepository.GetDefault(x => x.Id == id);
                if (deleteReply == null)
                {
                    return false;
                }
                else
                {
                    return await replyRepository.Delete(deleteReply);
                }
            }
        }

        public async Task<bool> UpdateReply(UpdateReplyDTO updateReplyDTO)
        {
            if (updateReplyDTO == null)
            {
                return false;
            }
            else
            {
                Reply updateComment = await replyRepository.GetDefault(x => x.Id == updateReplyDTO.Id);
                if (updateComment == null)
                {
                    return false;
                }
                else
                {
                    updateComment = mapper.Map(updateReplyDTO, updateComment);
                    return await replyRepository.Update(updateComment);
                }

            }
        }
    }
}

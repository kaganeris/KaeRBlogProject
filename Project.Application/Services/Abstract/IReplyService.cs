using Project.Application.Models.DTOs.ReplyDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Services.Abstract
{
    public interface IReplyService
    {
        Task<bool> CreateReply(CreateReplyDTO createReplyDTO);
        Task<bool> UpdateReply(UpdateReplyDTO updateReplyDTO);
        Task<bool> DeleteReply(int id);
        Task<List<ReplyVM>> GetAllReplys();
    }
}

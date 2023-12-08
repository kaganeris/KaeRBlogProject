using Project.Application.Models.DTOs.PostDTOs;
using Project.Application.Models.VMs.PostVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Services.Abstract
{
    public interface IPostService
    {
        Task<List<PostDetailVM>> GetDetailPostList();
        Task<UpdatePostDTO> GetPostById(int id);

        Task<bool> CreatePost(CreatePostDTO createPostDTO);
        Task<bool> UpdatePost(UpdatePostDTO updatePostDTO);
        Task<bool> DeletePost(int id);
    }
}

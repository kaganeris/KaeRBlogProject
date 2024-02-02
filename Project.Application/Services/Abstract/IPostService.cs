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
        Task<PostDetailVM> GetDetailPost(int postId,Guid userId);
        Task<UpdatePostDTO> GetPostById(int id);

        Task<bool> CreatePost(CreatePostDTO createPostDTO);
        Task<bool> UpdatePost(UpdatePostDTO updatePostDTO);
        Task<bool> DeletePost(int id);

        Task<List<PostHeroDTO>> GetHeroPosts();

        Task<PostGridVM> GetPostGridVM(string genreName,Guid userId);
        Task<PostGridVM> GetRandomPost(string genreName,Guid userId);
        Task<List<PostGridVM>> GetTrendingPosts();
        Task<List<PostGridVM>> GetPopulerPosts();
        Task<List<PostGridVM>> GetLatestPosts();
        Task<List<PostGridVM>> GetSectionPosts(string genreName,Guid userId);
        Task<List<PostDetailVM>> GetProfilePosts(Guid userId);
        Task<List<PostGridVM>> GetCategoryPostsByPageNumber(string categoryName,int pageNumber, Guid userId);
        Task IncreaseClickCount(int id);
        Task<int> GetPostCount();

        Task<List<PostListDTO>> GetPostList();
    }
}

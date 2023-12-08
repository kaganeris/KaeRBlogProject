using AutoMapper;
using Project.Application.Models.DTOs.PostDTOs;
using Project.Application.Models.VMs.PostVMs;
using Project.Application.Services.Abstract;
using Project.Domain.Entities;
using Project.Domain.Repositories;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Services.Concrete
{
    public class PostManager : IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly IMapper mapper;

        public PostManager(IPostRepository postRepository, IMapper mapper)
        {
            this.postRepository = postRepository;
            this.mapper = mapper;
        }
        public async Task<bool> CreatePost(CreatePostDTO createPostDTO)
        {
            if (createPostDTO == null)
                return false;
            else
            {
                using var image = Image.Load(createPostDTO.UploadPath.OpenReadStream());

                image.Mutate(x => x.Resize(300, 300)); // Görsel ile ilgili işlemler Mutate() içerisinde yapılabilir. Boyutlandırma, kırpma vs.

                Guid guid = Guid.NewGuid(); // Resmin ismini unique oluşturacağız.

                image.Save($"wwwroot/images/postPhotos/{guid}.jpg"); // wwwroot klasörünün içinde images içinde guid bir isimle dosya kaydedilsin.

                createPostDTO.ImagePath = $"/images/postPhotos/{guid}.jpg";

                Post post = mapper.Map<Post>(createPostDTO);
                await postRepository.Create(post);
                return true;
            }
        }

        public async Task<bool> DeletePost(int id)
        {
            if (id <= 0)
                return false;
            else
            {
                Post post = await postRepository.GetDefault(x => x.Id == id);
                if (post == null)
                    return false;
                else
                {
                    await postRepository.Delete(post);
                    return true;
                }
            }
        }

        public Task<List<PostDetailVM>> GetDetailPostList()
        {
            throw new NotImplementedException();
        }

        public async Task<UpdatePostDTO> GetPostById(int id)
        {
            if (id <= 0)
                return null;
            else
            {
                Post post = await postRepository.GetDefault(x => x.Id == id);
                UpdatePostDTO updatePostDTO = mapper.Map<UpdatePostDTO>(post);
                return updatePostDTO;
            }
        }

        public async Task<bool> UpdatePost(UpdatePostDTO updatePostDTO)
        {
            if (updatePostDTO == null) return false;
            else
            {
                Post post = await postRepository.GetDefault(x => x.Id == updatePostDTO.Id);
                post = mapper.Map(updatePostDTO, post);
                await postRepository.Update(post);
                return true;
            }
        }
    }
}

﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Models.DTOs.AuthorDTOs;
using Project.Application.Models.DTOs.CommentDTOs;
using Project.Application.Models.DTOs.PostDTOs;
using Project.Application.Models.DTOs.ReplyDTOs;
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

                //image.Mutate(x => x.Resize(300, 300)); // Görsel ile ilgili işlemler Mutate() içerisinde yapılabilir. Boyutlandırma, kırpma vs.

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

        public async Task<PostDetailVM> GetDetailPost(int postid, Guid userId)
        {
            PostDetailVM postDetailVM = await postRepository.GetFilteredFirstOrDefault(
                select: x => new PostDetailVM
                {
                    PostId = x.Id,
                    AppUserId = x.Author.AppUserId,
                    Title = x.Title,
                    Content = x.Content,
                    ImagePath = x.ImagePath,
                    CreatedDate = x.CreatedDate,
                    GenreName = x.Genre.Name,
                    ClickCount = x.ClickCount,
                    LikeCount = x.Likes.Count,
                    IsLiked = x.Likes.Any(x => x.AppUserId == userId),
                    Comments = x.Comments.Select(x => new CommentDTO
                    {
                        CommentId = x.Id,
                        Content = x.Content,
                        AppUserFullName = x.AppUser.FullName,
                        AppUserImagePath = x.AppUser.ImagePath,
                        CreatedDate = x.CreatedDate,
                        Replies = x.Replies.Select(x => new ReplyDTO
                        {
                            Content = x.Content,
                            AppUserImagePath = x.AppUser.ImagePath,
                            AppUserFullName = x.AppUser.FullName,
                            CreatedDate = x.CreatedDate
                        }).ToList(),
                    }).ToList(),
                },
                where: x => x.Id == postid && x.Status != Domain.Enums.Status.Passive,
                include: x => x.Include(x => x.Genre).Include(x => x.Author).ThenInclude(x => x.AppUser).Include(x => x.Comments)
                );

            return postDetailVM;
        }

        public async Task<List<PostHeroDTO>> GetHeroPosts()
        {
            List<PostHeroDTO> postHeroDTOs = await postRepository.GetFilteredList(
                select: x => new PostHeroDTO
                {
                    PostId = x.Id,
                    Title = x.Title,
                    ImagePath = x.ImagePath,
                    Content = x.Content.Substring(0, 100)
                },
                where: x => x.Status == Domain.Enums.Status.Active || x.Status == Domain.Enums.Status.Modified,
                orderBy: x => x.OrderByDescending(x => x.ClickCount)
                );
            postHeroDTOs = postHeroDTOs.Take(4).ToList();
            return postHeroDTOs;
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

        public async Task<PostGridVM> GetPostGridVM(string genreName, Guid userId)
        {
            PostGridVM postGridVM = await postRepository.GetFilteredFirstOrDefault(
                select: x => new PostGridVM
                {
                    PostId = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    ImagePath = x.ImagePath,
                    CreatedDate = x.CreatedDate,
                    AuthorFullName = x.Author.AppUser.FullName,
                    AuthorPhoto = x.Author.AppUser.ImagePath,
                    GenreName = x.Genre.Name,
                    IsLiked = x.Likes.Any(x => x.AppUserId == userId),
                },
                where: x => x.Genre.Name.ToLower() == genreName.ToLower() && x.Status != Domain.Enums.Status.Passive,
                orderBy: x => x.OrderByDescending(x => x.ClickCount),
                include: x => x.Include(x => x.Author).Include(x => x.Author.AppUser).Include(x => x.Genre)
                );
            return postGridVM;
        }

        public async Task<PostGridVM> GetRandomPost(string genreName, Guid userId)
        {
            List<PostGridVM> postGridVMList = await postRepository.GetFilteredList(
                select: x => new PostGridVM
                {
                    PostId = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    ImagePath = x.ImagePath,
                    CreatedDate = x.CreatedDate,
                    AuthorFullName = x.Author.AppUser.FullName,
                    AuthorPhoto = x.Author.AppUser.ImagePath,
                    GenreName = x.Genre.Name,
                    IsLiked = x.Likes.Any(x => x.AppUserId == userId),
                },
                where: x => x.Genre.Name.ToLower() == genreName.ToLower() &&
                            x.Status != Domain.Enums.Status.Passive,
                include: x => x.Include(x => x.Author).Include(x => x.Author.AppUser).Include(x => x.Genre)
            );


            if (postGridVMList != null && postGridVMList.Any())
            {
                var random = new Random();
                int randomIndex = random.Next(0, postGridVMList.Count);
                return postGridVMList[randomIndex];
            }

            return await GetRandomPost(genreName, userId);
        }

        public async Task<List<PostGridVM>> GetTrendingPosts()
        {
            DateTime lastWeek = DateTime.Now.AddDays(-7); // Geçen haftanın başlangıç tarihi
            DateTime now = DateTime.Now; // Şu anki tarih

            List<PostGridVM> postGridVM = await postRepository.GetFilteredList(
                select: x => new PostGridVM
                {
                    PostId = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    ImagePath = x.ImagePath,
                    CreatedDate = x.CreatedDate,
                    AuthorFullName = x.Author.AppUser.FullName,
                    AuthorPhoto = x.Author.AppUser.ImagePath,
                    GenreName = x.Genre.Name,
                },
                where: x => x.Status != Domain.Enums.Status.Passive /*&&
                            x.CreatedDate >= lastWeek && x.CreatedDate <= now*/, // Geçen hafta içinde olanlar
                orderBy: x => x.OrderByDescending(x => x.Likes.Count), // Beğeni sayısına göre sırala
                include: x => x.Include(x => x.Author).Include(x => x.Author.AppUser).Include(x => x.Genre)
            );

            return postGridVM.Take(6).ToList();
        }
        public async Task<List<PostGridVM>> GetPopulerPosts()
        {

            List<PostGridVM> postGridVM = await postRepository.GetFilteredList(
                select: x => new PostGridVM
                {
                    PostId = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    ImagePath = x.ImagePath,
                    CreatedDate = x.CreatedDate,
                    AuthorFullName = x.Author.AppUser.FullName,
                    AuthorPhoto = x.Author.AppUser.ImagePath,
                    GenreName = x.Genre.Name,
                },
                where: x => x.Status != Domain.Enums.Status.Passive /*&&
                            x.CreatedDate >= lastWeek && x.CreatedDate <= now*/, // Geçen hafta içinde olanlar
                orderBy: x => x.OrderByDescending(x => x.ClickCount), // Tıklanma sayısına göre sırala
                include: x => x.Include(x => x.Author).Include(x => x.Author.AppUser).Include(x => x.Genre)
            );

            return postGridVM.Take(6).ToList();
        }
        public async Task<List<PostGridVM>> GetLatestPosts()
        {

            List<PostGridVM> postGridVM = await postRepository.GetFilteredList(
                select: x => new PostGridVM
                {
                    PostId = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    ImagePath = x.ImagePath,
                    CreatedDate = x.CreatedDate,
                    AuthorFullName = x.Author.AppUser.FullName,
                    AuthorPhoto = x.Author.AppUser.ImagePath,
                    GenreName = x.Genre.Name,
                },
                where: x => x.Status != Domain.Enums.Status.Passive,
                orderBy: x => x.OrderByDescending(x => x.CreatedDate), // Tıklanma sayısına göre sırala
                include: x => x.Include(x => x.Author).Include(x => x.Author.AppUser).Include(x => x.Genre)
            );

            return postGridVM.Take(6).ToList();
        }

        public async Task<List<PostGridVM>> GetSectionPosts(string genreName, Guid userId)
        {
            DateTime lastWeek = DateTime.Now.AddDays(-7); // Geçen haftanın başlangıç tarihi
            DateTime now = DateTime.Now; // Şu anki tarih

            List<PostGridVM> postGridVM = await postRepository.GetFilteredList(
                select: x => new PostGridVM
                {
                    PostId = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    ImagePath = x.ImagePath,
                    CreatedDate = x.CreatedDate,
                    AuthorFullName = x.Author.AppUser.FullName,
                    AuthorPhoto = x.Author.AppUser.ImagePath,
                    GenreName = x.Genre.Name,
                    IsLiked = x.Likes.Any(x => x.AppUserId == userId),
                },
                where: x => x.Genre.Name.ToLower() == genreName.ToLower() &&
                            x.Status != Domain.Enums.Status.Passive /*&&
                            x.CreatedDate >= lastWeek && x.CreatedDate <= now*/, // Geçen hafta içinde olanlar
                orderBy: x => x.OrderByDescending(x => x.Likes.Count), // Beğeni sayısına göre sırala
                include: x => x.Include(x => x.Author).Include(x => x.Author.AppUser).Include(x => x.Genre)
            );

            return postGridVM.Take(15).ToList();
        }

        public async Task<List<PostDetailVM>> GetProfilePosts(Guid userId)
        {

            List<PostDetailVM> postDetailVMList = await postRepository.GetFilteredList(
                select: x => new PostDetailVM
                {
                    PostId = x.Id,
                    AppUserId = x.Author.AppUserId,
                    Title = x.Title,
                    Content = x.Content,
                    ImagePath = x.ImagePath,
                    CreatedDate = x.CreatedDate,
                    AuthorFullName = x.Author.AppUser.FullName,
                    AuthorPhoto = x.Author.AppUser.ImagePath,
                    GenreName = x.Genre.Name,
                    ClickCount = x.ClickCount,
                    LikeCount = x.Likes.Count,
                    IsLiked = x.Likes.Any(x => x.AppUserId == userId),
                    Comments = x.Comments.Select(x => new CommentDTO
                    {
                        CommentId = x.Id,
                        Content = x.Content,
                        AppUserFullName = x.AppUser.FullName,
                        CreatedDate = x.CreatedDate,
                        Replies = x.Replies.Select(x => new ReplyDTO
                        {
                            Content = x.Content,
                            AppUserImagePath = x.AppUser.ImagePath,
                            AppUserFullName = x.AppUser.FullName,
                            CreatedDate = x.CreatedDate
                        }).ToList(),
                    }).ToList(),
                },
                where: x => x.Status != Domain.Enums.Status.Passive && x.Author.AppUser.Id == userId,
                include: x => x.Include(x => x.Genre).Include(x => x.Author).ThenInclude(x => x.AppUser).Include(x => x.Comments)
                );

            return postDetailVMList;
        }

        public async Task IncreaseClickCount(int id)
        {
            Post post = await postRepository.GetDefault(x => x.Id == id);
            post.ClickCount++;
            await postRepository.Update(post);
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

        public async Task<List<PostGridVM>> GetCategoryPostsByPageNumber(string categoryName, int pageNumber, Guid userId)
        {
            List<PostGridVM> postGridVM = await postRepository.GetFilteredList(
               select: x => new PostGridVM
               {
                   PostId = x.Id,
                   Title = x.Title,
                   Content = x.Content,
                   ImagePath = x.ImagePath,
                   CreatedDate = x.CreatedDate,
                   AuthorFullName = x.Author.AppUser.FullName,
                   AuthorPhoto = x.Author.AppUser.ImagePath,
                   GenreName = x.Genre.Name,
                   IsLiked = x.Likes.Any(x => x.AppUserId == userId),
               },
               where: x => x.Genre.Name.ToLower() == categoryName.ToLower() &&
                           x.Status != Domain.Enums.Status.Passive,
               orderBy: x => x.OrderByDescending(x => x.Likes.Count), // Beğeni sayısına göre sırala
               include: x => x.Include(x => x.Author).Include(x => x.Author.AppUser).Include(x => x.Genre)
           );

            return postGridVM.Skip((pageNumber * 5) - 5).Take(5).ToList();
        }

        public async Task<int> GetPostCount()
        {
            List<Post> post = await postRepository.GetFilteredList(select: x => new Post { Id = x.Id }, where: x => x.Status != Domain.Enums.Status.Passive);

            return post.Count;
        }

        public async Task<List<PostListDTO>> GetPostList()
        {
            List<PostListDTO> postListDTO = await postRepository.GetFilteredList(
               select: x => new PostListDTO
               {
                   PostId = x.Id,
                   Title = x.Title,
                   AuthorName = x.Author.AppUser.FullName,
                   Content = x.Content,
                   ImagePath = x.ImagePath,
                   AuthorPhoto = x.Author.AppUser.ImagePath,
                   GenreName = x.Genre.Name,
                   CreatedDate = x.CreatedDate,
                   UpdatedDate = x.UpdatedDate,
                   DeletedDate = x.DeletedDate,
                   Status = x.Status
               },
               where: x => x.Id != 0,
               orderBy: x => x.OrderByDescending(x => x.CreatedDate),
               include: x => x.Include(x => x.Genre).Include(x => x.Author.AppUser).Include(x => x.Author)
           );

            return postListDTO;
        }

        public async Task<bool> ActivePost(int id)
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
                    post.Status = Domain.Enums.Status.Active;
                    await postRepository.Update(post);
                    return true;
                }
            }
        }
    }
}

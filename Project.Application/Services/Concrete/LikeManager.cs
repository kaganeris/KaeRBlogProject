﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Models.DTOs.LikeDTOs;
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
    public class LikeManager : ILikeService
    {
        private readonly ILikeRepository likeRepository;
        private readonly IMapper mapper;

        public LikeManager(ILikeRepository likeRepository, IMapper mapper)
        {
            this.likeRepository = likeRepository;
            this.mapper = mapper;
        }

        public async Task<bool> CreateLike(CreateLikeDTO createLikeDTO)
        {
            if (createLikeDTO == null)
            {
                return false;
            }
            else
            {
                Like like = mapper.Map<Like>(createLikeDTO);
                return await likeRepository.Create(like);
            }
        }

        public async Task<bool> DeleteLike(int id)
        {
            if (id <= 0)
            {
                return false;
            }
            else
            {
                Like like = await likeRepository.GetDefault(x => x.Id == id);
                if (like == null)
                {
                    return false;
                }
                else
                {
                    return await likeRepository.HardDelete(like);
                }
            }
        }

        public async Task<List<LikeVM>> GetAllLikes()
        {
            return await likeRepository.GetFilteredList(
              select: x => new LikeVM
              {
                  LikeId = x.Id,
                  PostName = x.Post.Title,
                  AppUserFullName = x.AppUser.FullName,
                  AppUserImagePath = x.AppUser.ImagePath,
                  CreatedDate = x.CreatedDate,
                  UpdatedDate = x.UpdatedDate,
                  DeletedDate = x.DeletedDate,
                  Status = x.Status,
              },
              where: x => x.Id != 0, orderBy: x => x.OrderByDescending(x => x.CreatedDate), include: x => x.Include(x => x.AppUser).Include(x => x.Post)
              );
        }

        public async Task<int> GetLikeId(int postId, Guid appUserId)
        {
            bool result = await likeRepository.Any(x => x.PostId == postId && x.AppUserId == appUserId);
            if (result)
            {
                Like like = await likeRepository.GetDefault(x => x.PostId == postId && x.AppUserId == appUserId);
                return like.Id;
            }
            else
                return 0;
        }

        public async Task<bool> UpdateLike(UpdateLikeDTO updateLikeDTO)
        {
            if (updateLikeDTO == null)
            {
                return false;
            }
            else
            {
                Like updatedLike = await likeRepository.GetDefault(x => x.Id == updateLikeDTO.Id);
                if (updatedLike == null)
                {
                    return false;
                }
                else
                {
                    updatedLike = mapper.Map(updateLikeDTO, updatedLike);
                    return await likeRepository.Update(updatedLike);
                }
            }
        }
    }
}

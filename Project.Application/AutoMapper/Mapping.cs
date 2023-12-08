﻿using AutoMapper;
using Project.Application.Models.DTOs.AppUserDTOs;
using Project.Application.Models.DTOs.AuthorDTOs;
using Project.Application.Models.DTOs.GenreDTOs;
using Project.Application.Models.DTOs.PostDTOs;
using Project.Application.Models.VMs.AuthorVMs;
using Project.Application.Models.VMs.GenreVMs;
using Project.Application.Models.VMs.PostVMs;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AppUser, RegisterDTO>().ReverseMap();
            CreateMap<AppUser, UpdateUserDTO>().ReverseMap();
            CreateMap<AppUser, UpdateUserDetailDTO>().ReverseMap();

            CreateMap<Author, CreateAuthorDTO>().ReverseMap();
            CreateMap<Author, UpdateAuthorDTO>().ReverseMap();
            CreateMap<Author, AuthorVM>().ReverseMap();
            CreateMap<Author, AuthorDetailVM>().ReverseMap();

            CreateMap<Genre, CreateGenreDTO>().ReverseMap();
            CreateMap<Genre, UpdateGenreDTO>().ReverseMap();
            CreateMap<Genre, GenreVM>().ReverseMap();


            CreateMap<Post, CreatePostDTO>().ReverseMap();
            CreateMap<Post, UpdatePostDTO>().ReverseMap();
            //CreateMap<Post, PostVM>().ReverseMap();
            //CreateMap<Post, GetPostVM>().ReverseMap();
            CreateMap<Post, PostDetailVM>().ReverseMap();
        }
    }
}

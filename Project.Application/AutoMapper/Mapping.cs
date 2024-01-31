using AutoMapper;
using Project.Application.Models.DTOs.AppUserDTOs;
using Project.Application.Models.DTOs.AuthorDTOs;
using Project.Application.Models.DTOs.CommentDTOs;
using Project.Application.Models.DTOs.GenreDTOs;
using Project.Application.Models.DTOs.LikeDTOs;
using Project.Application.Models.DTOs.PostDTOs;
using Project.Application.Models.DTOs.ReplyDTOs;
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
            CreateMap<AppUser, UpdateUserDetailDTO>().ReverseMap()
                .ForMember(dest => dest.ImagePath, opt => opt.Condition(src => src.ImagePath != null))
                .ForMember(dest => dest.FirstName, opt => opt.Condition(src => src.FirstName != null))
                .ForMember(dest => dest.LastName, opt => opt.Condition(src => src.LastName != null))
                .ForMember(dest => dest.BirthDate, opt => opt.Condition(src => src.BirthDate != null))
                .ForMember(dest => dest.About, opt => opt.Condition(src => src.About != null))
                .ForMember(dest => dest.Gender, opt => opt.Condition(src => src.Gender != null));

            CreateMap<Author, CreateAuthorDTO>().ReverseMap();
            CreateMap<Author, UpdateAuthorDTO>().ReverseMap();
            CreateMap<Author, AuthorVM>().ReverseMap();
            CreateMap<Author, AuthorDetailVM>().ReverseMap();

            CreateMap<Genre, CreateGenreDTO>().ReverseMap();
            CreateMap<Genre, UpdateGenreDTO>().ReverseMap();
            CreateMap<Genre, GenreVM>().ReverseMap();


            CreateMap<Post, CreatePostDTO>().ReverseMap();
            CreateMap<Post, UpdatePostDTO>().ReverseMap()
                .ForMember(dest => dest.Title, opt => opt.Condition(src => src.Title != null))
                .ForMember(dest => dest.Content, opt => opt.Condition(src => src.Content != null))
                .ForMember(dest => dest.ImagePath, opt => opt.Condition(src => src.ImagePath != null));

            //CreateMap<Post, PostVM>().ReverseMap();
            //CreateMap<Post, GetPostVM>().ReverseMap();
            CreateMap<Post, PostDetailVM>().ReverseMap();


            CreateMap<Comment, CreateCommentDTO>().ReverseMap();
            CreateMap<Comment, UpdateCommentDTO>().ReverseMap();

            CreateMap<Reply, CreateReplyDTO>().ReverseMap();
            CreateMap<Reply, UpdateReplyDTO>().ReverseMap();

            CreateMap<Like, CreateLikeDTO>().ReverseMap();
            CreateMap<Like, UpdateLikeDTO>().ReverseMap();
        }
    }
}

using AutoMapper;
using Project.Application.Models.DTOs.AppUserDTOs;
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
        }
    }
}

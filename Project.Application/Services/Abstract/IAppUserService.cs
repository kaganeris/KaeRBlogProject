using Microsoft.AspNetCore.Identity;
using Project.Application.Models.DTOs.AppUserDTOs;
using Project.Application.Models.DTOs.ConfirmMailDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Services.Abstract
{
    public interface IAppUserService
    {
        Task<IdentityResult> Register(RegisterDTO model);
        Task<SignInResult> Login(LoginDTO model);
        Task Logout();
        Task<UpdateUserDTO> GetByUserName(string userName);
        Task UpdateUser(UpdateUserDTO model);
        Task<bool> EmailConfirm(ConfirmMailDTO confirmMailDTO);
        Task<string> GetUserEmail(LoginDTO loginDTO);

        Task UpdateUserDetail(UpdateUserDetailDTO model);
        Task<List<UserDTO>> GetAllUsers(int pageNumber);

        Task<List<UserDTO>> UserCount();
        Task<UserDTO> GetAdminInfo();
    }
}

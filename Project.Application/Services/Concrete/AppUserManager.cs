using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Project.Application.Helper;
using Project.Application.Models.DTOs.AppUserDTOs;
using Project.Application.Models.DTOs.ConfirmMailDTOs;
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
    public class AppUserManager : IAppUserService
    {
        private readonly IAppUserRepository appUserRepository;
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;

        // Dependency Injection
        public AppUserManager(IAppUserRepository appUserRepository, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMapper mapper)
        {
            this.appUserRepository = appUserRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        // UserName ile AppUser tablosunda bulunan (eğer varsa) AppUser satırını çekeriz ve UpdateProfileDTO nesnesini doldururuz.
        public async Task<UpdateUserDTO> GetByUserName(string userName)
        {
            UpdateUserDTO result = await appUserRepository.GetFilteredFirstOrDefault(
                select: x => new UpdateUserDTO
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Password = x.PasswordHash,
                    Email = x.Email,
                },
                where: x => x.UserName == userName
                );

            //AppUser appUser = await userManager.FindByNameAsync(userName);
            //UpdateAppUserDTO deneme = mapper.Map<UpdateAppUserDTO>(appUser);

            return result;
        }

        // Kullanıcının sisteme login olmasını sağlayacak. User bilgilerine DTO'dan ulaştık. Ve bu bilgileri Session'da tutabiliriz.
        public async Task<SignInResult> Login(LoginDTO model)
        {
            AppUser appUser = await userManager.FindByNameAsync(model.UserName);
            if (appUser != null && appUser.EmailConfirmed == true)
            {
                return await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
            }
            else if (appUser == null)
                return SignInResult.Failed;
            else
                return SignInResult.NotAllowed;
        }

        // Kullanıcının sistemden çıkış yapması için kullanılır. User bilgileri session'dan silinir.
        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        // Yeni bir AppUser oluştururuz.
        public async Task<IdentityResult> Register(RegisterDTO model)
        {
            // Gelen RegisterDTO, Create edilmesi gereken AppUser. AutoMapper kullanacağız.
            AppUser user = new AppUser();
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.CreatedDate = DateTime.Now;
            user.Status = Domain.Enums.Status.Active;

            Random random = new Random();
            int code = random.Next(100000, 1000000);
            user.ConfirmCode = code;

            IdentityResult result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                MailHelper.Send(user.Email, code);
                await userManager.AddToRoleAsync(user, "User");
            }

            return result;
        }

        // Kullanıcı bilgilerini güncellemek için kullanacağımız metot.
        // Kullanıcının güncellemek istediği bilgileri View'dan UpdateProfileDTO nesneni aracılığıyla alacağız. Resim, Email,Password alanlarını kontrol ederek AppUser nesnesinde güncelleme yapacağız.

        public async Task UpdateUser(UpdateUserDTO model)
        {
            // Update işlemlerinde önce Id ile ilgili nesneyi RAM'e çekeriz. Dışarıdan gelen güncel bilgilerle değişiklikleri yaparız. En son SaveChanges ile veritabanına güncellemeleri göndeririz.

            //Mapper
            var user = mapper.Map<AppUser>(model);

            AppUser user2 = await appUserRepository.GetDefault(x => x.Id == user.Id);
           

            await appUserRepository.Update(user2);

            // Parola değiştirme işlemi

            if (model.Password != null)
            {
                user2.PasswordHash = userManager.PasswordHasher.HashPassword(user2, model.Password);
                await userManager.UpdateAsync(user2);
            }

            // Email adres işlemleri (Eğer yoksa ekleteceğiz)
            if (model.Email != null)
            {
                AppUser isUserEmailExits = await userManager.FindByEmailAsync(model.Email);

                if (isUserEmailExits == null)
                    await userManager.SetEmailAsync(user2, model.Email);
            }
        }

        public async Task<bool> EmailConfirm(ConfirmMailDTO confirmMailDTO)
        {
            if (confirmMailDTO.Email != null)
            {
                var user = await userManager.FindByEmailAsync(confirmMailDTO.Email);
                if (user != null && user.ConfirmCode == confirmMailDTO.ConfirmCode)
                {
                    user.EmailConfirmed = true;
                    await userManager.UpdateAsync(user);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public async Task<string> GetUserEmail(LoginDTO loginDTO)
        {
            AppUser appUser = await userManager.FindByNameAsync(loginDTO.UserName);
            if (appUser != null)
                return appUser.Email;
            else
                return "Hata!";
        }

        public async Task UpdateUserDetail(UpdateUserDetailDTO model)
        {
            AppUser appUser = await userManager.FindByIdAsync(model.Id.ToString());

            if (appUser != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());

                image.Mutate(x => x.Resize(300, 300));

                Guid guid = Guid.NewGuid();

                image.Save($"wwwroot/images/profilePhotos/{guid}.jpg"); 

                model.ImagePath = $"/images/profilePhotos/{guid}.jpg";

                appUser = mapper.Map(model, appUser);

               await userManager.UpdateAsync(appUser);
            }
        }
    }
}


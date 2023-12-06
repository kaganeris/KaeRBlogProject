using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Project.Domain.Entities;
using Project.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Project.Presentation.Models.SeedData
{
    public static class DataSeeder
    {
        public static async void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                AppDbContext context = serviceScope.ServiceProvider.GetService<AppDbContext>();


                context.Database.Migrate();
                
                if (!context.Roles.Any())
                {
                    
                    await context.Roles.AddRangeAsync(
                        new AppRole() { Name = "User", NormalizedName = "USER" },
                        new AppRole() { Name = "Admin", NormalizedName = "ADMIN" },
                        new AppRole() { Name = "Author", NormalizedName = "AUTHOR" }
                        );
                }
                await context.SaveChangesAsync();

                if (!context.Users.Any())
                {
                    PasswordHasher<AppUser> password = new();
                    AppUser appUser = new AppUser() { FirstName = "Admin", LastName = "Admin", UserName = "admin", NormalizedUserName = "ADMIN", BirthDate = new DateTime(1990, 01, 01), Gender = Domain.Enums.Gender.Male, Email = "admin@admin.com", NormalizedEmail = "ADMIN@ADMIN.COM", EmailConfirmed = true, SecurityStamp = Guid.NewGuid().ToString("D") };

                    context.Users.AddRange(appUser);
                    var hashed = password.HashPassword(appUser, "Admin12.");
                    appUser.PasswordHash = hashed;

                    await context.SaveChangesAsync();

                    await AssignRoles((IServiceProvider)serviceScope, appUser.Email, "ADMIN");
                }
                await context.SaveChangesAsync();
            }
        }
        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string role)
        {
            UserManager<AppUser> _userManager = services.GetService<UserManager<AppUser>>();
            AppUser user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRoleAsync(user, role);

            return result;
        }
    }
}

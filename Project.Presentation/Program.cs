using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Application.IoC;
using Project.Application.SeedData;
using Project.Application.Validations;
using Project.Domain.Entities;
using Project.Infrastructure.Context;
using Project.Presentation.Models.SeedData;
using System;

namespace Project.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultCon")));

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule(new DependencyResolver()); // Kendi oluþturduðumuz Resolver Class'ýn instance'ýný alýyoruz.
            });

            builder.Services.AddIdentity<AppUser, AppRole>().AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider).AddEntityFrameworkStores<AppDbContext>().AddErrorDescriber<CustomIdentityValidator>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Home/AccessDenied";

                options.LogoutPath = "/User/Logout";

                options.LoginPath = "/User/Login";


            });

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;

                options.Password.RequireUppercase = true;

                options.Password.RequireDigit = true;

                options.Password.RequireLowercase = true;

                options.Password.RequireNonAlphanumeric = true;

                options.User.RequireUniqueEmail = true;


            });

            var app = builder.Build();

            //SEEDDATA ADMÝN 
            var serviceScope = app.Services.CreateScope();
            AppDbContext _context = serviceScope.ServiceProvider.GetService<AppDbContext>()!;
            UserManager<AppUser> userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>()!;
            RoleManager<AppRole> roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>()!;

            AdminSeedData.Seed(userManager, roleManager, _context);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //DataSeeder.Seed(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
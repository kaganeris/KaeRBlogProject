using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using Project.Infrastructure.EntityTypeConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.ApplyConfiguration(new AppUserConfig());
            builder.ApplyConfiguration(new AuthorConfig());
            builder.ApplyConfiguration(new CommentConfig());
            builder.ApplyConfiguration(new GenreConfig());
            builder.ApplyConfiguration(new LikeConfig());
            builder.ApplyConfiguration(new PostConfig());
            builder.ApplyConfiguration(new ReplyConfig());

            base.OnModelCreating(builder);
        }
    }
}

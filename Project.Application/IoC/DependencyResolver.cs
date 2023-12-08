using Autofac;
using AutoMapper;
using Project.Application.AutoMapper;
using Project.Application.Services.Abstract;
using Project.Application.Services.Concrete;
using Project.Domain.Repositories;
using Project.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.IoC
{
    public class DependencyResolver : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<Mapper>().As<IMapper>().InstancePerLifetimeScope();


            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AuthorRepository>().As<IAuthorRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PostRepository>().As<IPostRepository>().InstancePerLifetimeScope();
            builder.RegisterType<GenreRepository>().As<IGenreRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CommentRepository>().As<ICommentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<LikeRepository>().As<ILikeRepository>().InstancePerLifetimeScope();


            builder.RegisterType<AppUserManager>().As<IAppUserService>().InstancePerLifetimeScope();
            builder.RegisterType<GenreManager>().As<IGenreService>().InstancePerLifetimeScope();
            builder.RegisterType<PostManager>().As<IPostService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthorManager>().As<IAuthorService>().InstancePerLifetimeScope();

            #region AutoMapper
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Mapping>(); // AutoMapper klasörünün altına eklediğimiz mapping class'ını ekliyoruz.
            })).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            }).As<IMapper>().InstancePerLifetimeScope();
            #endregion


            base.Load(builder);
        }
    }
}


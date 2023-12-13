using Project.Domain.Entities;
using Project.Domain.Enums;
using Project.Domain.Repositories;
using Project.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repositories
{
    public class LikeRepository : BaseRepository<Like>, ILikeRepository
    {
        private readonly AppDbContext context;

        public LikeRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> HardDelete(Like entity)
        {
            try
            {
                context.Likes.Remove(entity);
                return await context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;

            }
        }
    }
}

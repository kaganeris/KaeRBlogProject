using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Repositories
{
    public interface ILikeRepository : IBaseRepository<Like>
    {
        Task<bool> HardDelete(Like entity);
    }
}

﻿using Project.Domain.Entities;
using Project.Domain.Repositories;
using Project.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repositories
{
    public class ReplyRepository : BaseRepository<Reply>, IReplyRepository
    {
        public ReplyRepository(AppDbContext context) : base(context)
        {

        }
    }
}

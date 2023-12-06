using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.EntityTypeConfig
{
    public class LikeConfig : BaseEntityConfig<Like>
    {
        public override void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.AppUser).WithMany(x => x.Likes).HasForeignKey(x => x.AppUserId);
            builder.HasOne(x => x.Post).WithMany(x => x.Likes).HasForeignKey(x => x.PostId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

            base.Configure(builder);
        }
    }
}

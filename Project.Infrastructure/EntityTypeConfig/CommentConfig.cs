using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.EntityTypeConfig
{
    public class CommentConfig : BaseEntityConfig<Comment>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Content).IsRequired(true);
            builder.HasOne(x => x.AppUser).WithMany(x => x.Comments).HasForeignKey(x => x.AppUserId);
            builder.HasOne(x => x.Post).WithMany(x => x.Comments).HasForeignKey(x => x.PostId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            base.Configure(builder);
        }
    }
}

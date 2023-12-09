using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.EntityTypeConfig
{
    public class ReplyConfig : BaseEntityConfig<Reply>
    {
        public override void Configure(EntityTypeBuilder<Reply> builder)
        {
            builder.Property(x => x.Content).IsRequired(true);
            builder.HasOne(x => x.AppUser).WithMany(x => x.Replies).HasForeignKey(x => x.AppUserId);
            builder.HasOne(x => x.Comment).WithMany(x => x.Replies).HasForeignKey(x => x.CommentId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            base.Configure(builder);
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.EntityTypeConfig
{
    public class AuthorConfig : BaseEntityConfig<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.AppUser).WithOne(x => x.Author).HasForeignKey<Author>(x => x.AppUserId);
        }
    }
}

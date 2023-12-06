using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.EntityTypeConfig
{
    public class AppUserConfig : BaseEntityConfig<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).IsRequired(true);
            builder.Property(x => x.Email).IsRequired(true);
            builder.Property(x => x.ImagePath).IsRequired(false).HasMaxLength(250);
            base.Configure(builder);
        }
    }
}

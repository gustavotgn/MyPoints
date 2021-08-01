using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPoints.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Data.Mapping
{
    public class ApplicationUserMap : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Id).HasColumnType("uniqueidentifier");

            builder.Property(bc => bc.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(bc => bc.UpdatedDate).HasDefaultValueSql("GETUTCDATE()");

            builder.Property(bc => bc.CreatedUser).IsRequired(false).HasColumnType("NVARCHAR(256)");
            builder.Property(bc => bc.UpdatedUser).IsRequired(false).HasColumnType("NVARCHAR(256)");

            builder.Property(bc => bc.IsExcluded).HasColumnType("bit").HasDefaultValueSql("0");

            builder.ToTable("User");
        }
    }
}

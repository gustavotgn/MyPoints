using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPoints.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Data.Mapping
{
    public class UserAddressMap : BaseEntityMap<UserAddress,Guid>, IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            //base.Configure(builder);
            builder.Property(x => x.Id).HasColumnType("uniqueidentifier");

            builder.Property(x => x.Complements).HasColumnType("NVARCHAR(255)");
            builder.Property(x => x.Street).HasColumnType("NVARCHAR(255)");
            builder.Property(x => x.City).HasColumnType("NVARCHAR(255)");
            builder.Property(x => x.State).HasColumnType("NVARCHAR(255)");
            builder.Property(x => x.PostalCode).HasColumnType("NVARCHAR(255)");
            builder.Property(x => x.Number).HasColumnType("NVARCHAR(255)");
            builder.Property(x => x.CreatedDate).HasColumnType("NVARCHAR(255)");

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class OfficeConfiguration : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("office");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Address1)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("address1");
            builder.Property(e => e.Address2)
                .HasMaxLength(50)
                .HasColumnName("address2");
            builder.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("city");
            builder.Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("country");
            builder.Property(e => e.OfficineCode)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("officineCode");
            builder.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("phone");
            builder.Property(e => e.Region)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("region");
            builder.Property(e => e.ZipCode)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("zipCode");
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class ProducttypeConfiguration : IEntityTypeConfiguration<Producttype>
{
    public void Configure(EntityTypeBuilder<Producttype> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("producttype");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.DescriptionHtml)
                .HasColumnType("text")
                .HasColumnName("descriptionHtml");
            builder.Property(e => e.DescriptionText)
                .HasColumnType("text")
                .HasColumnName("descriptionText");
            builder.Property(e => e.Image)
                .HasMaxLength(256)
                .HasColumnName("image");
            builder.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("type");

    }
}

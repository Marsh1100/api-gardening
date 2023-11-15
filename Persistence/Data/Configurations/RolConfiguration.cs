using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class RolConfiguration : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {
        builder.ToTable("rol");

        builder.Property(p => p.Id)
            .IsRequired();
        
        builder.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.HasIndex(p=> p.Name)
            .IsUnique();

    }

}

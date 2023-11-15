using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Persistence.Data.Configurations;

public class RequestdetailConfiguration : IEntityTypeConfiguration<Requestdetail>
{
    public void Configure(EntityTypeBuilder<Requestdetail> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("requestdetail");

            builder.HasIndex(e => e.IdProduct, "idProduct");

            builder.HasIndex(e => e.IdRequest, "idRequest");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.IdProduct).HasColumnName("idProduct");
            builder.Property(e => e.IdRequest).HasColumnName("idRequest");
            builder.Property(e => e.LineNumber).HasColumnName("lineNumber");
            builder.Property(e => e.Quantity).HasColumnName("quantity");
            builder.Property(e => e.UnitPrice)
                .HasPrecision(15, 2)
                .HasColumnName("unitPrice");

            builder.HasOne(d => d.IdProductNavigation).WithMany(p => p.Requestdetails)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("requestdetail_ibfk_2");

            builder.HasOne(d => d.IdRequestNavigation).WithMany(p => p.Requestdetails)
                .HasForeignKey(d => d.IdRequest)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("requestdetail_ibfk_1");

    }
}

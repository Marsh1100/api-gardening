using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Persistence.Data.Configurations;

public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
         builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("request");

            builder.HasIndex(e => e.IdClient, "idClient");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.DeliveryDate).HasColumnName("deliveryDate");
            builder.Property(e => e.ExpectedDate).HasColumnName("expectedDate");
            builder.Property(e => e.Feedback)
                .HasColumnType("text")
                .HasColumnName("feedback");
            builder.Property(e => e.IdClient).HasColumnName("idClient");
            builder.Property(e => e.RequestDate).HasColumnName("requestDate");
            builder.Property(e => e.State)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("state");

            builder.HasOne(d => d.IdClientNavigation).WithMany(p => p.Requests)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("request_ibfk_1");

    }
}

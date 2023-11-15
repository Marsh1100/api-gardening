using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("payment");

            builder.HasIndex(e => e.IdClient, "idClient");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.IdClient).HasColumnName("idClient");
            builder.Property(e => e.PaymentDate).HasColumnName("paymentDate");
            builder.Property(e => e.PaymentMethod)
                .IsRequired()
                .HasMaxLength(40)
                .HasColumnName("paymentMethod");
            builder.Property(e => e.Total)
                .HasPrecision(15, 2)
                .HasColumnName("total");
            builder.Property(e => e.TransactionId)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("transactionId");

            builder.HasOne(d => d.IdClientNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_ibfk_1");

    }
}

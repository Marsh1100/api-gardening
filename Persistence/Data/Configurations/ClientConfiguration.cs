using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("client");

            builder.HasIndex(e => e.IdEmployee, "idEmployee");

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
                .HasMaxLength(50)
                .HasColumnName("city");
            builder.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            builder.Property(e => e.CreditLimit)
                .HasPrecision(15, 2)
                .HasColumnName("creditLimit");
            builder.Property(e => e.Fax)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("fax");
            builder.Property(e => e.IdEmployee).HasColumnName("idEmployee");
            builder.Property(e => e.LastnameContact)
                .HasMaxLength(50)
                .HasColumnName("lastnameContact");
            builder.Property(e => e.NameClient)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("nameClient");
            builder.Property(e => e.NameContact)
                .HasMaxLength(50)
                .HasColumnName("nameContact");
            builder.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("phoneNumber");
            builder.Property(e => e.Region)
                .HasMaxLength(50)
                .HasColumnName("region");
            builder.Property(e => e.ZipCode)
                .HasMaxLength(10)
                .HasColumnName("zipCode");

            builder.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("client_ibfk_1");

    }
}

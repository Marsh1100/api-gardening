using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("employee");

            builder.HasIndex(e => e.IdBoss, "idBoss");

            builder.HasIndex(e => e.IdOffice, "idOffice");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("email");
            builder.Property(e => e.Extension)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("extension");
            builder.Property(e => e.FirstSurname)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("firstSurname");
            builder.Property(e => e.IdBoss).HasColumnName("idBoss");
            builder.Property(e => e.IdOffice).HasColumnName("idOffice");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
            builder.Property(e => e.Position)
                .HasMaxLength(50)
                .HasColumnName("position");
            builder.Property(e => e.SecondSurname)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("secondSurname");

            builder.HasOne(d => d.IdBossNavigation).WithMany(p => p.InverseIdBossNavigation)
                .HasForeignKey(d => d.IdBoss)
                .HasConstraintName("employee_ibfk_2");

            builder.HasOne(d => d.IdOfficeNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdOffice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_ibfk_1");

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");

        builder.Property(p=> p.Id)
            .IsRequired();
        
        builder.Property(p=> p.IdenNumber)
            .HasMaxLength(20)
            .IsRequired();
        builder.HasIndex(p => p.IdenNumber)
            .IsUnique();
        builder.Property(p=> p.UserName)
            .HasMaxLength(50)
            .IsRequired();
        builder.HasIndex(p => p.UserName)
            .IsUnique();
        builder.Property(p=> p.Email)
            .HasMaxLength(100)
            .IsRequired();
        builder.HasIndex(p => p.Email)
            .IsUnique();
        builder.Property(p=>p.Password)
            .HasMaxLength(255)
            .IsRequired();
        
        builder
            .HasMany(p=>p.Roles)
            .WithMany(p=>p.Users)
            .UsingEntity<UserRol>(
                j=> j
                    .HasOne(t=> t.Rol)
                    .WithMany(m=> m.UsersRoles)
                    .HasForeignKey(f=> f.RolId),
                j=> j
                    .HasOne(t=> t.User)
                    .WithMany(m=>m.UsersRoles)
                    .HasForeignKey(f=> f.UserId),
                j=>
                {
                    j.ToTable("userRol");
                    j.HasKey(t=> new {t.UserId, t.RolId});
                }
            );

    }
}

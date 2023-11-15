using System;
using System.Collections.Generic;
using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public partial class GardeningContext : DbContext
{
    public GardeningContext()
    {
    }

    public GardeningContext(DbContextOptions<GardeningContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<UserRol> UserRoles { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Office> Offices { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Producttype> Producttypes { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Requestdetail> Requestdetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }


   
}

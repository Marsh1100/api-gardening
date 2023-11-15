using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("product");

            builder.HasIndex(e => e.IdProductType, "idProductType");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            builder.Property(e => e.Dimensions)
                .HasMaxLength(25)
                .HasColumnName("dimensions");
            builder.Property(e => e.IdProductType).HasColumnName("idProductType");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(70)
                .HasColumnName("name");
            builder.Property(e => e.ProductCode)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("productCode");
            builder.Property(e => e.Provider)
                .HasMaxLength(50)
                .HasColumnName("provider");
            builder.Property(e => e.ProviderPrice)
                .HasPrecision(15, 2)
                .HasColumnName("providerPrice");
            builder.Property(e => e.SalePrice)
                .HasPrecision(15, 2)
                .HasColumnName("salePrice");
            builder.Property(e => e.Stock).HasColumnName("stock");

            builder.HasOne(d => d.IdProductTypeNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdProductType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_ibfk_1");

    }
}

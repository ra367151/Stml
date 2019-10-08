using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stml.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Datas.EntityTypeConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "dbo")
                .HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(50)")
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(c => c.Price)
                .HasColumnType("Price")
                .HasColumnType("decimal(18,2)");
        }
    }
}

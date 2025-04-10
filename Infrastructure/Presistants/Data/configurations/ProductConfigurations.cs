﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistants.Data.configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.TypeId);

            builder.HasOne(p => p.ProductBrand)
               .WithMany()
               .HasForeignKey(p => p.BrandId);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}

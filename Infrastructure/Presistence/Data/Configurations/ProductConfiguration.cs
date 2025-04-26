using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Presistence.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductBrand)
                   .WithMany()
                   .HasForeignKey(p => p.BrandId)
                   .OnDelete(DeleteBehavior.NoAction); 

            builder.HasOne(p => p.ProductType)
                   .WithMany()
                   .HasForeignKey(p => p.TypeId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(P=>P.Price).HasColumnType("decimal(10,2)");
        }
    }
}

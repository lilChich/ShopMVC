using Microsoft.EntityFrameworkCore;
using ShopMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Configuration
{
    public class PurchaseCompositionMapping : IEntityTypeConfiguration<CompositionPurchase>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CompositionPurchase> builder)
        {
            builder.ToTable("PurchaseComposition", "dbo");         

            builder
                .HasOne(x => x.Purchase)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.PurchaseId);
            builder
                 .HasOne(x => x.Product)
                 .WithMany(x => x.Purchases)
                 .HasForeignKey(x => x.ProductId);
            builder
                .HasKey(x => new { x.ProductId, x.PurchaseId});

        }
    }
}

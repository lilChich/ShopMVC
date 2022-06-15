using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Configuration
{
    public class PurchaseMapping : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase", "dbo");

            builder.HasKey(x => new { x.Id });

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Purchases)
                .HasForeignKey(x => x.UserId);

            builder.Property(x => x.Date).HasColumnType("datetime");
            
        }
    }
}

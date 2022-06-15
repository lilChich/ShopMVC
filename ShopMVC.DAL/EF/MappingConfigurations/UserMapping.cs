using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopMVC.DAL.Entities;

namespace ShopMVC.EF.Configuration
{
    public class UserMapping : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("AspNetUsers", "dbo");

            builder.HasKey(x => new { x.Id });
        }
    }
}

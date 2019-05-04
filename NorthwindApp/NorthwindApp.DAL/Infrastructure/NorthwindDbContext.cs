using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NorthwindApp.DAL.Entities;

namespace NorthwindApp.DAL.Infrastructure
{
    public class NorthwindDbContext : IdentityDbContext
    {
        public NorthwindDbContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductDto>()
                .ToTable("Products");

            modelBuilder.Entity<SupplierDto>()
                .ToTable("Suppliers");

            modelBuilder.Entity<CategoryDto>()
                .ToTable("Categories")
                .HasOne(x => x.Details)
                .WithOne(x => x.Category)
                .HasForeignKey<CategoryImageDetailsDto>(x => x.CategoryId);

            modelBuilder.Entity<CategoryImageDetailsDto>()
                .ToTable("Categories")
                .HasOne(x => x.Category)
                .WithOne(x => x.Details)
                .HasForeignKey<CategoryDto>(x => x.CategoryId);
        }
    }
}

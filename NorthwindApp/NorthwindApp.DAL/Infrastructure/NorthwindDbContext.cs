using Microsoft.EntityFrameworkCore;
using NorthwindApp.DAL.Entities;

namespace NorthwindApp.DAL.Infrastructure
{
    public class NorthwindDbContext : DbContext
    {
        public NorthwindDbContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<ProductDto> Products { get; set; }

        public DbSet<CategoryDto> Categories { get; set; }

        public DbSet<SupplierDto> Suppliers { get; set; }
    }
}

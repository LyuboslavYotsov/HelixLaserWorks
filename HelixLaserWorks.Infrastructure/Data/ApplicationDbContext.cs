using HelixLaserWorks.Infrastructure.Data.Configuration;
using HelixLaserWorks.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HelixLaserWorks.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Material> Materials { get; set; } = null!;

        public DbSet<Thickness> Thicknesses { get; set; } = null!;

        public DbSet<Part> Parts { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<Offer> Offers { get; set; } = null!;

        public DbSet<Review> Reviews { get; set; } = null!;

        public DbSet<MaterialType> MaterialTypes { get; set; } = null!;

        public DbSet<MaterialThickness> MaterialsThicknesses { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PartConfiguration());
            builder.ApplyConfiguration(new ThicknessesConfiguration());
            builder.ApplyConfiguration(new MaterialTypeConfiguration());
            builder.ApplyConfiguration(new MaterialConfiguration());
            builder.ApplyConfiguration(new MaterialsThicknessesConfiguration());
            builder.ApplyConfiguration(new ReviewConfiguration());

            base.OnModelCreating(builder);
        }
    }
}

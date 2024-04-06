using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelixLaserWorks.Infrastructure.Data.Configuration
{
    internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        private SeedData? _seedData;

        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            _seedData = new SeedData();

            builder.HasData(_seedData.AdminRole);
        }
    }
}

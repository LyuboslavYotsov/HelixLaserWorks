using HelixLaserWorks.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelixLaserWorks.Infrastructure.Data.Configuration
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        private SeedData? _seedData;
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            _seedData = new SeedData();

            builder.HasData(_seedData.AdminUserRole);
        }
    }
}

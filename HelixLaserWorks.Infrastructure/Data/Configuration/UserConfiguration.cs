using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelixLaserWorks.Infrastructure.Data.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        private SeedData? _seedData;

        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            _seedData = new SeedData();

            builder.HasData(_seedData.AdminUser, _seedData.CustomerUser);
        }
    }
}

using HelixLaserWorks.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelixLaserWorks.Infrastructure.Data.Configuration
{
    public class ThicknessesConfiguration : IEntityTypeConfiguration<Thickness>
    {
        private SeedData? _seedData;

        public void Configure(EntityTypeBuilder<Thickness> builder)
        {
            _seedData = new SeedData();

            builder.HasData(_seedData.Thicknesses);
        }
    }
}

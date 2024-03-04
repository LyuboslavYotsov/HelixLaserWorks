using HelixLaserWorks.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelixLaserWorks.Infrastructure.Data.Configuration
{
    public class MaterialTypeConfiguration : IEntityTypeConfiguration<MaterialType>
    {
        private SeedData? _seedData;

        public void Configure(EntityTypeBuilder<MaterialType> builder)
        {
            _seedData = new SeedData();

            builder.HasData(_seedData.MetalType, _seedData.PlasticType, _seedData.WoodType);
        }
    }
}

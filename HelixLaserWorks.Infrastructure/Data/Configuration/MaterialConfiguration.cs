using HelixLaserWorks.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelixLaserWorks.Infrastructure.Data.Configuration
{
    internal class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        private SeedData? _seedData;

        public void Configure(EntityTypeBuilder<Material> builder)
        {
            _seedData = new SeedData();

            builder.HasData(_seedData.MildSteel, _seedData.StainlessSteel, _seedData.Aluminum, _seedData.Copper);
        }


    }
}

using HelixLaserWorks.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelixLaserWorks.Infrastructure.Data.Configuration
{
    public class MaterialsThicknessesConfiguration : IEntityTypeConfiguration<MaterialThickness>
    {
        private SeedData? _seedData;

        public void Configure(EntityTypeBuilder<MaterialThickness> builder)
        {
            _seedData = new SeedData();

            builder
                .HasKey(mt => new { mt.MaterialId, mt.ThicknessId });

            builder.HasData(_seedData.MildSteelThichnesses);
            builder.HasData(_seedData.StainlessSteelThicknesses);
            builder.HasData(_seedData.AluminumThicknesses);
            builder.HasData(_seedData.CopperThicknesses);
        }
    }
}

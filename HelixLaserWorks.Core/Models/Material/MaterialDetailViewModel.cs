namespace HelixLaserWorks.Core.Models.Material
{
    public class MaterialDetailViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Price { get; set; } = string.Empty;

        public string Density { get; set; } = string.Empty;

        public string? Specification { get; set; }

        public string Rusting { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string AvailableThicknesses { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }
    }
}

namespace HelixLaserWorks.Core.Models.Material
{
    public class MaterialViewModel
    {
        public int Id { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }
    }
}

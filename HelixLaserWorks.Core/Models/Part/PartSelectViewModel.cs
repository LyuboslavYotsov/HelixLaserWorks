namespace HelixLaserWorks.Core.Models.Part
{
    public class PartSelectViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string PartMaterial { get; set; } = string.Empty;

        public double PartThickness { get; set; }

        public int Quantity { get; set; }

        public string SchemeUrl { get; set; } = string.Empty;
    }
}
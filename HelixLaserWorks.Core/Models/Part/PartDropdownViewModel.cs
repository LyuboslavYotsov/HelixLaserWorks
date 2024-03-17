namespace HelixLaserWorks.Core.Models.Part
{
    public class PartDropdownViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string PartMaterial { get; set; } = string.Empty;

        public double PartThickness { get; set; }
    }
}
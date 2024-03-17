namespace HelixLaserWorks.Core.Models.Parts
{
    public class PartViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Material { get; set; } = string.Empty;

        public double Thickness { get; set; }

        public int Quantity { get; set; }

        public string SchemeFilePath { get; set; } = string.Empty;

        public string CreatedOn { get; set; } = string.Empty;

        public string UpdatedOn { get; set; } = string.Empty;
    }
}

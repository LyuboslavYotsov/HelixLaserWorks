using System.ComponentModel.DataAnnotations;
using static HelixLaserWorks.Infrastructure.Data.Constants.DataConstants;

namespace HelixLaserWorks.Core.Models.Material
{
    public class MaterialFormModel
    {
        [Required]
        [StringLength(MaterialNameMaxLength,
            MinimumLength = MaterialNameMinLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(MaterialDescriptionMaxLength,
            MinimumLength = MaterialDescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [StringLength(MaterialSpecificationMaxLenth,
            MinimumLength = MaterialSpecificationMinLenth)]
        public string? Specification { get; set; }

        [Required]
        [Range(MaterialDensityMinValue, MaterialDensityMaxValue)]
        public double Density { get; set; }

        [Required]
        [Display(Name = "Price Per Square Meter")]
        [Range(MaterialPricePerSquareMeterMinValue, MaterialPricePerSquareMeterMaxValue)]
        public decimal PricePerSquareMeter { get; set; }

        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Corosion Resistance")]
        public bool CorrosionResistance { get; set; }

        [Required]
        public int MaterialTypeId { get; set; }

        public ICollection<MaterialTypeViewModel> MaterialTypes { get; set; } = new List<MaterialTypeViewModel>();

        public ICollection<double> AvailableThicknesses { get; set; } = new List<double>();

        public ICollection<double> SelectedThicknesses { get; set; } = new List<double>();
    }
}

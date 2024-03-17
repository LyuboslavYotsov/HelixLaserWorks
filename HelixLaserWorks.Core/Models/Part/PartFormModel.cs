using System.ComponentModel.DataAnnotations;
using HelixLaserWorks.Core.Models.Material;
using static HelixLaserWorks.Infrastructure.Data.Constants.DataConstants;

namespace HelixLaserWorks.Core.Models.Part
{
    public class PartFormModel
    {
        [Required]
        [StringLength(PartNameMaxLength,
            MinimumLength = PartNameMinLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(PartDescriptionMaxLength, 
            MinimumLength = PartDescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int MaterialId { get; set; }

        public ICollection<MaterialDropdownViewModel> Materials { get; set; } = new List<MaterialDropdownViewModel>();

        [Required]
        public double PartThickness { get; set; }

        [Required]
        [Range(PartQuantityMinValue, PartQuantityMaxValue)]
        public int Quantity { get; set; }

        public string? SchemeUrl { get; set; }
    }
}

using HelixLaserWorks.Infrastructure.Data.Models.Enumerators;
using System.ComponentModel.DataAnnotations;
using static HelixLaserWorks.Infrastructure.Data.Constants.DataConstants;

namespace HelixLaserWorks.Infrastructure.Data.Models
{
    public class Material
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public MaterialType Type { get; set; }

        [Required]
        [MaxLength(MaterialNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public double Thickness { get; set; }

        [Required]
        [MaxLength(MaterialSpecificationMaxLenth)]
        public string Specification { get; set; } = string.Empty;

        [Required]
        public double Density { get; set; }

        [Required]
        public decimal PricePerSquareMeter { get; set; }
    }
}

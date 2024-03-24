using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HelixLaserWorks.Infrastructure.Data.Constants.DataConstants;

namespace HelixLaserWorks.Infrastructure.Data.Models
{
    public class Material
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MaterialTypeId { get; set; }

        [Required]
        [ForeignKey(nameof(MaterialTypeId))]
        public MaterialType MaterialType { get; set; } = null!;

        [Required]
        [MaxLength(MaterialNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(MaterialDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        public ICollection<MaterialThickness> MaterialThicknesses { get; set; } = new List<MaterialThickness>();

        [MaxLength(MaterialSpecificationMaxLenth)]
        public string? Specification { get; set; }

        [Required]
        public bool CorrosionResistance { get; set; }

        [Required]
        public double Density { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerSquareMeter { get; set; }

        [Required]
        public string ImageUrl { get; set; } = string.Empty;
    }
}

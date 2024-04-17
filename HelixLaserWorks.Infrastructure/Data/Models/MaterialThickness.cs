using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelixLaserWorks.Infrastructure.Data.Models
{
    public class MaterialThickness
    {
        [Required]
        public int MaterialId { get; set; }

        [ForeignKey(nameof(MaterialId))]
        public Material Material { get; set; } = null!;

        [Required]
        public int ThicknessId { get; set; }

        [ForeignKey(nameof(ThicknessId))]
        public Thickness Thickness { get; set; } = null!;
    }
}

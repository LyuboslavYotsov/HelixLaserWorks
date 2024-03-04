using System.ComponentModel.DataAnnotations;
using static HelixLaserWorks.Infrastructure.Data.Constants.DataConstants;

namespace HelixLaserWorks.Infrastructure.Data.Models
{
    public class MaterialType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaterialTypeNameMaxLength)]
        public string Name { get; set; } = string.Empty;
    }
}

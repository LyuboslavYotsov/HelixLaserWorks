using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HelixLaserWorks.Infrastructure.Data.Constants.DataConstants;

namespace HelixLaserWorks.Infrastructure.Data.Models
{
    public class Part
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PartNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(PartDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int MaterialId { get; set; }

        [ForeignKey(nameof(MaterialId))]
        public Material Material { get; set; } = null!;

        [Required]
        public double Thickness { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string SchemeURL { get; set; } = null!;

        [Required]
        public string CreatorId { get; set; } = string.Empty;

        public IdentityUser Creator { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime UpdatedOn { get; set; }

        public int? OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }
    }
}

using HelixLaserWorks.Infrastructure.Data.Models.Enumerators;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HelixLaserWorks.Infrastructure.Data.Constants.DataConstants;

namespace HelixLaserWorks.Infrastructure.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        [MaxLength(OrderDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string CustomerId { get; set; } = string.Empty;

        [ForeignKey(nameof(CustomerId))]
        public IdentityUser Customer { get; set; } = null!;

        public ICollection<Part> Parts { get; set; } = new List<Part>();
    }
}

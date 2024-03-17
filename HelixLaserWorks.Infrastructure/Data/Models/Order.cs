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
        [MaxLength(OrderTitleMaxLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(OrderDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string CustomerId { get; set; } = string.Empty;

        [ForeignKey(nameof(CustomerId))]
        public IdentityUser Customer { get; set; } = null!;

        public ICollection<Part> Parts { get; set; } = new List<Part>();

        public int? OfferId { get; set; }

        [ForeignKey(nameof(OfferId))]
        public Offer? Offer { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}

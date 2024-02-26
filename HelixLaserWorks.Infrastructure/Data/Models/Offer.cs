using System.ComponentModel.DataAnnotations;
using static HelixLaserWorks.Infrastructure.Data.Constants.DataConstants;

namespace HelixLaserWorks.Infrastructure.Data.Models
{
    public class Offer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public Order Order { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime DeliveryDueDate { get; set; }

        [MaxLength(OfferNotesMaxLength)]
        public string? Notes { get; set; }
    }
}

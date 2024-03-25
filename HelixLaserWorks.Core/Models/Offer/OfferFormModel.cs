using HelixLaserWorks.Core.Models.Order;
using System.ComponentModel.DataAnnotations;
using static HelixLaserWorks.Infrastructure.Data.Constants.DataConstants;

namespace HelixLaserWorks.Core.Models.Offer
{
    public class OfferFormModel
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public OrderViewModel Order { get; set; } = null!;

        [Required]
        [Range(OfferPriceMinValue, OfferPriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        public DateTime DeliveryDueDate { get; set; }

        public string? Notes { get; set; }
    }
}

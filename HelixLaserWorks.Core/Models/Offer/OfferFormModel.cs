using HelixLaserWorks.Core.Models.Order;
using System.ComponentModel.DataAnnotations;
using static HelixLaserWorks.Infrastructure.Data.Constants.DataConstants;

namespace HelixLaserWorks.Core.Models.Offer
{
    public class OfferFormModel
    {
        [Required]
        public int OrderId { get; set; }

        public OrderViewModel? Order { get; set; }

        [Required]
        [Range(OfferPriceMinValue, OfferPriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Production Days")]
        [Range(OfferProductionDaysMinValue, OfferProductionDaysMaxValue)]
        public int ProductionDays { get; set; }

        public string? Notes { get; set; }
    }
}

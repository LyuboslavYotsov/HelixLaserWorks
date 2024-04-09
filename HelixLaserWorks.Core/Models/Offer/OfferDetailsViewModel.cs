using HelixLaserWorks.Core.Models.Order;

namespace HelixLaserWorks.Core.Models.Offer
{
    public class OfferDetailsViewModel
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public string AdminNotes { get; set; } = string.Empty;

        public int ProductionDays { get; set; }

        public int OrderId { get; set; }

        public bool IsAccepted { get; set; }

        public string CreatedOn { get; set; } = string.Empty;

        public OrderViewModel Order { get; set; } = null!;

        public bool IsCustomerContacted { get; set; }
    }
}

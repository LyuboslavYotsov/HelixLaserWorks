using HelixLaserWorks.Core.Models.Order;

namespace HelixLaserWorks.Core.Models.Offer
{
    public class OfferViewModel
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public string DeliveryDueDate { get; set; } = string.Empty;

        public string AdminNotes { get; set; } = string.Empty;

        public int OrderId { get; set; }

        public OrderViewModel Order { get; set; } = null!;
    }
}

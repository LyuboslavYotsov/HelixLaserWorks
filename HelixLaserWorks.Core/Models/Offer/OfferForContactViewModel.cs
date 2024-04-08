namespace HelixLaserWorks.Core.Models.Offer
{
    public class OfferForContactViewModel
    {
        public int Id { get; set; }

        public string AdminNotes { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int ProductionDays { get; set; }

        public string OrderName { get; set; } = string.Empty;

        public string CreatedOn { get; set; } = string.Empty;

        public string CustomerPhone { get; set; } = string.Empty;

        public string CustomerEmail { get; set; } = string.Empty;
    }
}

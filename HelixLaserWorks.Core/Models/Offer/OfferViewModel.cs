namespace HelixLaserWorks.Core.Models.Offer
{
    public class OfferViewModel
    {
        public int Id { get; set; }

        public string AdminNotes { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int ProductionDays { get; set; }

        public string OrderName { get; set; } = string.Empty;

        public string CreatedOn { get; set; } = string.Empty;

        public bool IsAccepted { get; set; }

        public ICollection<string> PartsNames { get; set; } = new List<string>();
    }
}

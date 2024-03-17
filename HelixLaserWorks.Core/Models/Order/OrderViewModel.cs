using HelixLaserWorks.Core.Models.Part;

namespace HelixLaserWorks.Core.Models.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ICollection<PartDropdownViewModel> Parts { get; set; } = new List<PartDropdownViewModel>();

        public string Status { get; set; } = string.Empty;

        public string CreatedOn { get; set; } = string.Empty;

        public int? OfferId { get; set; }

        public string? AdminFeedback { get; set; } = string.Empty;
    }
}

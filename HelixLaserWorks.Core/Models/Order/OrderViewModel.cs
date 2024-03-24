using HelixLaserWorks.Core.Models.Part;
using System.ComponentModel.DataAnnotations;

namespace HelixLaserWorks.Core.Models.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Display(Name = "Phone Number")]
        public string CustomerPhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Customer Email")]
        public string CustomerEmail { get; set; } = string.Empty;

        public ICollection<PartSelectViewModel> Parts { get; set; } = new List<PartSelectViewModel>();

        public string Status { get; set; } = string.Empty;

        public string CreatedOn { get; set; } = string.Empty;

        public int? OfferId { get; set; }

        public string? AdminFeedback { get; set; } = string.Empty;
    }
}

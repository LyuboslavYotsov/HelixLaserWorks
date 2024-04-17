using System.ComponentModel.DataAnnotations;
using static HelixLaserWorks.Infrastructure.Data.Constants.DataConstants;

namespace HelixLaserWorks.Core.Models.Order
{
    public class OrderDeclineViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string UserEmail { get; set; } = string.Empty;

        [Required]
        [StringLength(OrderFeedbackMaxLength,
            MinimumLength = OrderFeedbackMinLength)]
        public string Feedback { get; set; } = string.Empty;
    }
}

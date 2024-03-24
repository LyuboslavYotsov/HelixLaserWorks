using HelixLaserWorks.Core.Models.Part;
using System.ComponentModel.DataAnnotations;
using static HelixLaserWorks.Infrastructure.Data.Constants.DataConstants;

namespace HelixLaserWorks.Core.Models.Order
{
    public class OrderFormModel
    {
        [Required]
        [StringLength(OrderTitleMaxLength,
            MinimumLength = OrderTitleMinLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(OrderDescriptionMaxLength,
            MinimumLength = OrderDescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(OrderPhoneMaxLength,
            MinimumLength = OrderDescriptionMinLength)]
        [Phone]
        public string CustomerPhoneNumber { get; set; } = string.Empty;

        public ICollection<PartDropdownViewModel> UserParts { get; set; } = new List<PartDropdownViewModel>();

        public ICollection<int> SelectedParts { get; set; } = new List<int>();
    }
}

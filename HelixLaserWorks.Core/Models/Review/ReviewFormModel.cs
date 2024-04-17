using System.ComponentModel.DataAnnotations;
using static HelixLaserWorks.Infrastructure.Data.Constants.DataConstants;

namespace HelixLaserWorks.Core.Models.Review
{
    public class ReviewFormModel
    {
        [Required]
        [Range(ReviewRatingMinValue, ReviewRatingMaxValue)]
        public int Rating { get; set; }

        [StringLength(ReviewCommentMaxLength)]
        public string? Comment { get; set; }
    }
}

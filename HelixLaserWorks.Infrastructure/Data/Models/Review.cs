using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HelixLaserWorks.Infrastructure.Data.Constants.DataConstants;

namespace HelixLaserWorks.Infrastructure.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CustomerId { get; set; } = string.Empty;

        [Required]
        public IdentityUser Customer { get; set; } = null!;

        [Required]
        public int Rating { get; set; }

        [MaxLength(ReviewCommentMaxLength)]
        public string? Comment { get; set; }
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HelixLaserWorks.Infrastructure.Data.Constants.DataConstants;

namespace HelixLaserWorks.Infrastructure.Data.Models
{
    public class Offer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public Order Order { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [MaxLength(OfferNotesMaxLength)]
        public string? Notes { get; set; }

        [Required]
        public int ProductionDays { get; set; }

        [Required]
        public bool IsAccepted { get; set; } = false;

        public DateTime? AcceptedOn { get; set; }

        [Required]
        public bool IsCustomerContacted{ get; set; } = false;
    }
}
